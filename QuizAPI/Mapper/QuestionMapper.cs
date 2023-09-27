using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using QuizAPI.Data;
using QuizAPI.Extensions;

namespace QuizAPI.DataMapper;

public class QuestionMapper : IDataMapper<Question>
{
    // public Question MapOld(IDataRecord data) => new Question(
    //     Identifier: data.GetInt32(0),
    //     Prompt: data.GetString(1),
    //     Answer: data.GetString(2),
    //     Choices: new ()
    // );

    public Question Map(IDataRecord data)
    {
        return  data.Get<Question>("question");
    }
}
