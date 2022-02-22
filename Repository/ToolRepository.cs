using static ToolSpace.ToolManager;
using ToolSpace;
using Npgsql;
using Nest;
using System;
using Dapper;
using Esrecord;
using Newtonsoft.Json;
namespace Infra;

public class ToolRepository {
    private const string ConnectionString = @"Host=192.168.12.41;Port=7065;Database=postgres;Username=postgres;Password=postgres";
    public ToolContext  _context;
    
    public ToolRepository(){
        
    }
    
    public IEnumerable<ToolRecord> GetRecord(){
        var now = DateTime.Now;
        const string AnotherQuery =  @"select a.asset_type,a.id,a.last_modified_time,a.name,ma.machine_code from icnc_erp.assets a
        inner join icnc_erp.batches b on a.id = b.asset_id
        inner join icnc_erp.stock_changes s on b.id = s.batch_id
        inner join icnc_erp.manufacture_consume_stock_out_record ma on ma.id = s.id where a.last_modified_time +  INTERVAL '7 day'>=CURRENT_DATE";
        const string cor = @"where a.last_modified_time +  INTERVAL '7 day'>=CURRENT_DATE";
        using var conn = new NpgsqlConnection(ConnectionString);
        var recs = conn.Query<ToolRecord>(AnotherQuery).ToList();
        if (recs.Count()==0){
            return null;
        }
        return recs.AsEnumerable<ToolRecord>();
    }
    public ToolContext GetData(List<Guid> idlist){
        foreach (var id in idlist){
            const string query = @"select a.name,a.last_modified_time from icnc_erp.assets a where a.id=id";
            using var conn = new NpgsqlConnection(ConnectionString);
            var recs = conn.Query<ToolRecord>(query).ToList();
        }

        //add data to ToolContext
        // _context = new ToolContext();
        // foreach (var rec in recs){
        //     _context.name = rec.name;
        // }
        return _context;
    }
}