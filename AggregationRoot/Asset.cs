using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using Nest;

namespace ToolSpace {

    public class Asset  {
        public const string AssetInventoryReportBucket = "asset-inventory";

        ///<summary>
        /// discrimeter
        ///</summary>
        // public enum ASSET_TYPE : byte {
        //     [Description("一般资产")]
        //     Asset = 0,
        //     [Description("产品")]
        //     Product,
        //     [Description("半成品")]
        //     SemiFinishedProduct,
        //     [Description("原材料")]
        //     RawMaterial,
        //     [Description("刀具")]
        //     Tool,
        //     [Description("量具")]
        //     Gauge,
        //     [Description("油品")]
        //     Oil,
        //     [Description("杂货")]
        //     Zaka,
        //     [Description("包装材料")]
        //     PkgMaterial
        // }

        internal Asset()
        {
            CreationTime = DateTime.Now;
            LastModifiedTime = DateTime.Now;
        }

        public Asset(string code, string name)
        {
            (Code, Name) = (code, name);
            CreationTime = DateTime.Now;
            LastModifiedTime = DateTime.Now;
        }
        public int asset_type { get; set; }
        public string Code { get; set; }

        // public string UpdateCode(string code) => Code = code;

        // public UNIT Unit { get; init; }
        public string Name { get; set; }
        public Guid Id { get; set; }

        [PropertyName("creation_time")]
        public DateTime CreationTime { get; init; } = DateTime.Now;

        [NotMapped]
        [PropertyName("last_modified_time")]
        [Date]
        public DateTime LastModifiedTime { get; protected set; }

        // [Ignore] public ISet<SpecRecord> Specifications { get; set; } = new HashSet<SpecRecord>();

        // [JsonIgnore]
        // public string DescriptionInDetail
        // {
        //     get {
        //         switch (this)
        //         {
        //             case Product p:
        //                 {
        //                     var aliases = string.Join(";", p.Aliases);
        //                     return $"{aliases} {p.Name} {p.MaterialDescription} {p.FinishDescription} {p.HeatTreatmentDescription}";
        //                 }
        //             case RawMaterial rm:
        //                 {
        //                     return string.Join(' ', Specifications.Select(x => x.DescriptionFragment));
        //                 }
        //             default:
        //                 return Name;
        //         }
        //     }
        // }

        // [PropertyName("nick_name")]
        // public string NickName
        // {
        //     get {
        //         switch (this)
        //         {
        //             case Product product:
        //                 {
        //                     return string.Join(' ', product.Aliases) + ' ' + Name;
        //                 }
        //             case RawMaterial rawMaterial:
        //                 {
        //                     return rawMaterial.DescriptionInDetail;
        //                 }
        //             default:
        //                 return this.Name;
        //         }
        //     }

        // }
    }
}