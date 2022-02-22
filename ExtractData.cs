using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Collections;
using Npgsql;
using System.Web;
using Nest;
using ToolSpace;
using Infra;
using static ToolSpace.ToolManager;
using DataService.Controllers;
namespace ExtractData{
    public class ElasticSearch{
    //public List<ToolContext> ReturnTool;
    private readonly ElasticClient _client;
    private readonly ILogger<ElasticSearch> _logger;
    private readonly ILogger <dataQueryController> log;
    private readonly HttpContextAccessor _httpContextAccessor;
    //private readonly ToolRepository _toolRepo;

    public ElasticSearch(ILogger<ElasticSearch> logger){
        var settings = new ConnectionSettings(new Uri("http://192.168.12.41:9200")).DefaultIndex("defaultindex");
        _client = new ElasticClient(settings);
        _logger = logger;
        _logger.LogInformation("exec");
        //ReturnTool = new List<ToolContext>();
    }
    public IEnumerable<ToolContext> Query(string query){
        var conn = query.Split(" ");
        
        var resp = _client.Search<ToolRecord>(
            s => s.Size(100).Index("tool_search_").Query(
                q => q.Bool(
                    b => b.Should(
                        //for code
                        sb => sb.Term(ss => ss.Field("machine_code").Value($"{query}").Boost(100)),
                        sb => sb.Match(ss => ss.Field("machine_code").Query($"{query}")),
                        //for name
                        b => b.Bool(
                            s => s.Should(
                                sb => sb.Fuzzy(ss => ss.Field("name").Value($"{query}").Fuzziness(Fuzziness.Auto).MaxExpansions(42).PrefixLength(1).Transpositions()),
                                sb => sb.Wildcard(ss => ss.Field("name").Value($"*{query}*").Boost(42)),
                                sb => sb.Match(ss => ss.Field("name").Query($"{query}"))
                            )
                        )
                    )
                )
            )
        );
        //_logger.LogInformation(resp.ToString());

        
        List<Guid> id_list = new List<Guid>();
        List<ToolContext> contexts = new List<ToolContext>();

        
        
        foreach (var doc in resp.Documents){
            // _logger.LogInformation(doc.Id.ToString());
            // _logger.LogInformation(doc.name.ToString());
            // _logger.LogInformation(doc.machine_code.ToString());
            // ReturnTool.Add(this.GetData(doc.Id));
            contexts.Add(new ToolContext(doc.Id,doc.name,doc.machine_code));
            id_list.Add(doc.Id);
        }
        return contexts;
        // var toolRepo = new ToolRepository();
        // toolRepo.GetData(id_list);
        
    }
    // public ToolContext GetData(Guid id){
        
    //     var ToolRepo = new ToolRepository();
    //     var res = ToolRepo.GetData(id);
        
    //     //_logger.LogInformation(context.ToString());
    //     return res;
    // }
    // public IEnumerable<ToolContext> print(){
    //     foreach (var i in ReturnTool){
    //         _logger.LogInformation(i.Id.ToString());
    //     }
    //     return ReturnTool;
    // }
}
}