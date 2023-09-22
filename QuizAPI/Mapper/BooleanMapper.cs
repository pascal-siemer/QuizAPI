using System.Data;

namespace QuizAPI.DataMapper;

public class BooleanMapper : IDataMapper<bool>
{
    public bool Map(IDataRecord data) => data.GetBoolean(0);
}