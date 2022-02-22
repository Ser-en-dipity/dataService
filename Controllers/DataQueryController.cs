using Microsoft.AspNetCore.Mvc;
using icnc.erp.data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Collections;
using Npgsql;
using ExtractData;
using Nest;
using static ToolSpace.ToolManager;
using ToolSpace;
using System.Json;
using System.Text.Json;
using Newtonsoft.Json;
namespace DataService.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class dataQueryController : ControllerBase{
        public ElasticSearch _es;
        
        private readonly ILogger<dataQueryController> _logger;

        public dataQueryController(ILogger<dataQueryController> logger,ElasticSearch es){
            _logger = logger;   
            _es = es;
        }
       
        [HttpPost]
        public string Post([FromBody]string query){
            //_logger.LogInformation(query.ToString());
            var resp = _es.Query(query);
            // foreach(var res in resp){
            //     _logger.LogInformation(res.name.ToString());
            // }
            var jsontxt = JsonConvert.SerializeObject(resp, Formatting.Indented);
            
            return jsontxt;

        }
        // [HttpGet]
        // public IEnumerable<ToolContext> Get(Guid id){

        //     var res = _es.print();
        //     foreach(var tool in res.ToList()){
        //         //_logger.LogInformation(tool.name.ToString());
        //     }
        //     return res;
        // }
    }
}