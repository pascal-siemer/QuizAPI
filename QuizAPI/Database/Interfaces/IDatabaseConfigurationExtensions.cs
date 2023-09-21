namespace RestService.Configurations;

public static class IDatabaseConfigurationExtensions
{
    public static string GetSchema(this IDatabaseConfiguration configuration)
    {
        var schema = configuration?.Schema ?? string.Empty;
        
        if (schema == string.Empty)
        {
            return string.Empty;
        }

        return $"[{schema.Replace(".", "].[")}].";
    }
}