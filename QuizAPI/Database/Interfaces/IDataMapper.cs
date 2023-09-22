using System.Data;

namespace QuizAPI.DataMapper;

public interface IDataMapper<T>
{
    public T Map(IDataRecord data);
}