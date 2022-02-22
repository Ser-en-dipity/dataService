using Nest;
using System;
using Dapper;
using Npgsql;
using static ToolSpace.ToolManager;
using Infra;
using Esrecord;
namespace backgroundWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    public ElasticClient _client;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
        var settings = new ConnectionSettings(new Uri("http://192.168.12.41:9200")).DefaultIndex("defaultindex");
        _client = new ElasticClient(settings);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {       
            var indexCount = _client.Count<ToolRecord>(s => s.Index("tool_search_")).Count;
            
            var searchResponse = _client.Search<ToolRecord>(s =>
                s.Index("tool_search_").From(0).Size((int)indexCount).Query(q => q.MatchAll()).Fields("last_modified_time"));
            var records = searchResponse.Hits.Select(hit => new ToolRecord(Guid.Parse(hit.Id), hit.Fields.Value<DateTime>("last_modified_time"))).ToList();


            var ToolRepo = new ToolRepository();
            var recs = ToolRepo.GetRecord();
            
            //_client.IndexMany<ToolRecord>(recs,"tool_search_");
            if (recs != null){
            var dbRecords = recs.Select(x => 
                new ToolRecord(x.Id, x.last_modified_time) {machine_code = x.machine_code,name = x.name} );
            

            var recordsToDelete = records.Where(recordOfEs =>
                !dbRecords.Any(r => r.Id == recordOfEs.Id && r.last_modified_time.Date == recordOfEs.last_modified_time.Date));
            var recordsToAdd = dbRecords.Where(recordOfDb =>
                !records.Any(r => r.Id == recordOfDb.Id && r.last_modified_time.Date == r.last_modified_time.Date)); 
            
            if (recordsToAdd.Count() > 0) {
                var resp = _client.IndexMany(recordsToAdd, "tool_search_");
                _logger.LogInformation(resp.ToString());
            }
            foreach (var record in recordsToDelete) _client.Delete<ToolRecord>(record.Id, s => s.Index("tool_search_"));
            }
            
            await Task.Delay(5000, stoppingToken);
        }
    }
}
