using System.Data;

namespace RestService.DataMapper;

public interface IDataMapper<T>
{
    public T Map(IDataRecord data);
}