using Esrecord;
namespace Infra;

public interface IRepository{
    public IEnumerable<Record> GetRecord();
}