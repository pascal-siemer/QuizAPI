using System.Text.Json.Serialization;

namespace QuizAPI.Data;

public record Choice
{
    [JsonPropertyName("id")]
    public int Identifier { get; init; }
    
    [JsonPropertyName("title")]
    public string Title { get; init; }
    
    public Choice(int identifier, string title)
    {
        this.Identifier = identifier;
        this.Title = title;
    }
    
    public void Deconstruct(out int Identifier, out string Title)
    {
        Identifier = this.Identifier;
        Title = this.Title;
    }
}