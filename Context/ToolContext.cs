namespace ToolSpace;

public class ToolContext{
    public ToolContext(){}
    public ToolContext(Guid _id,string _name,string _machine){
        Id = _id;
        name = _name;
        machine_code = _machine;
    }
    public Guid Id;
    public string name;
    public string machine_code;
}