
namespace Services{
    public class assetService{
        public record AssetEsRecord : EsRecord {
            public AssetEsRecord(Guid id, DateTime lastModifiedTime) : base(id, lastModifiedTime) { }
            public string Code { get; set; }
            public string Name { get; set; }
            public string NameRaw { get; set; }

            [Nest.PropertyName("material_description")]
            public string[] MaterialDescription { get; set; }
            [Nest.PropertyName("material_specifications")]
            public string[] MaterialSpecifications { get; set; }
            [Nest.PropertyName("aliases")]
            public string[] Aliases { get; set; }
            [Nest.PropertyName("semi_code")]
            public string SemiCode { get; set; }
            [Nest.PropertyName("tool_stock_cell")]
            public string ToolStockCell { get; set; }
        }
    }
}