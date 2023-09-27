using System.Data;
using QuizAPI.Extensions;

namespace QuizAPI.DataMapper;

public class BooleanMapper : IDataMapper<bool>
{
    public bool Map(IDataRecord data) => data.Get<bool>("result");
}