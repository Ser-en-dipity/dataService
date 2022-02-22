using System;
using Dapper;

namespace icnc.erp.data{
    public class Assets{
        Guid AssetId{ get; set; }
        public enum ASSET_TYPEP{}

        public string Name { get; set; }

    }
    public class Batches{
        public Guid AssetId { get; }

        public string SerialNumber { get; }
        public decimal Quantity { get; }
        public decimal UnitPrice { get; }
    }
}