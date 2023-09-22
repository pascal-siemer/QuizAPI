using System.Data;

namespace QuizAPI.DataMapper;

public class StringMapper : IDataMapper<string>
{
    public string Map(IDataRecord data) => data.GetString(0);
}