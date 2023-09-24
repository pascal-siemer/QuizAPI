using System.Text.Json.Serialization;

namespace QuizAPI.Data;

public record Question
{
    [JsonPropertyName("id")]
    public int Identifier { get; init; }
    
    [JsonPropertyName("prompt")]
    public string Prompt { get; init; }
    
    [JsonPropertyName("answer")]
    public string Answer { get; init; }
    
    [JsonPropertyName("choices")]
    public List<Choice> Choices { get; init; }
    
    public Question(int identifier, string prompt, string answer, List<Choice> choices)
    {
        this.Identifier = identifier;
        this.Prompt = prompt;
        this.Answer = answer;
        this.Choices = choices;
    }

    public void Deconstruct(out int Identifier, out string Prompt, out string Answer, out List<Choice> Choices)
    {
        Identifier = this.Identifier;
        Prompt = this.Prompt;
        Answer = this.Answer;
        Choices = this.Choices;
    }
}