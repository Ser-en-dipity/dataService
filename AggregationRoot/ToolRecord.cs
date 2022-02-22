using Esrecord;
namespace ToolSpace{

public class ToolManager{
        public record ToolRecord : Record{
        public ToolRecord(){}
        public ToolRecord(Guid asset_id,DateTime last){
            Id = asset_id;
            last_modified_time = last;
        }
        public string machine_code;
        public string name;
        public DateTime last_modified_time;
        public string asset_type;
    }
}
}