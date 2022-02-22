using System;
using System.ComponentModel;

namespace ToolSpace {
    public class Tool  {
        public enum TOOL_TYPE : byte {
            [Description("主夹")] ZHU_JIA = 0,
            [Description("背夹")] BEI_JIA,
            [Description("导套")] DAO_TAO,
            [Description("料夹")] LIAO_JIA,
            [Description("夹头")] JIA_TOU,
            [Description("刀套")] ToolHolder,
            [Description("丝攻")] SI_GONG,
            [Description("钻头")] ZUAN_TOU,
            [Description("铣刀")] XI_DAO,
            [Description("铰刀")] JIAO_DAO,
            [Description("镗刀")] TANG_DAO,
            [Description("刀片")] DAO_PIAN,
            [Description("刀柄")] DAO_BING,
            [Description("配件")] PEI_JIAN,
            UN_KNOWN
        }

        protected Tool()
        {
        }
        public string Code ;
        public int id;
        public Guid machine_id;
        public Guid product_id;
        
        public Tool(string code, string name, TOOL_TYPE type)
        {
            ToolType = type;
        }

        public TOOL_TYPE ToolType { get; init; }
        public string StockCode { get; set; }
        public int? ToolLife { get; set; }

    }
}