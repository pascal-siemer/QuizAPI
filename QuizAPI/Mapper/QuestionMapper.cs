using System.Data;
using QuizAPI.Data;

namespace QuizAPI.DataMapper;

public class QuestionMapper : IDataMapper<Question>
{
    public Question Map(IDataRecord data) => new Question(
        Identifier: data.GetInt32(0),
        Prompt: data.GetString(1),
        Answer: data.GetString(2),
        Choices: new ()
    );
}
