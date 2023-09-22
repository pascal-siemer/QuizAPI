using System.Text.Json.Serialization;

namespace QuizAPI.Data;

public record Question
{

    [JsonPropertyName("identifier")]
    public int Identifier { get; init; }
    
    [JsonPropertyName("prompt")]
    public string Prompt { get; init; }
    
    [JsonPropertyName("answer")]
    public string Answer { get; init; }
    
    [JsonPropertyName("choices")]
    public List<string> Choices { get; init; }
    
    public Question(int Identifier, string Prompt, string Answer, List<string> Choices)
    {
        this.Identifier = Identifier;
        this.Prompt = Prompt;
        this.Answer = Answer;
        this.Choices = Choices;
    }

    public void Deconstruct(out int Identifier, out string Prompt, out string Answer, out List<string> Choices)
    {
        Identifier = this.Identifier;
        Prompt = this.Prompt;
        Answer = this.Answer;
        Choices = this.Choices;
    }
}