namespace QuizAPI.Configurations;

public interface IDatabaseConfiguration
{
    public string ConnectionString { get; set; }
    public string Schema { get; set; }
}