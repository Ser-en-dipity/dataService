namespace Esrecord;


public record Record{
    public Record(){}
    public Guid Id { get; init; }
    public string machine_code;
    public string name;
    public string asset_type;
    public DateTime last_modified_time;
}
