using Npgsql;

namespace Infarstructure;

public class Utilities
{
    public static readonly Uri Uri;
    public static readonly string ProperlyFormattedConnectionString;
    
    static Utilities()
    {
        string rawConnectionString;
        string envVarKeyName = "pgconn";

        rawConnectionString = Environment.GetEnvironmentVariable(envVarKeyName)!;
        if (rawConnectionString == null)
        {
            throw new Exception($@"
🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨
YOUR CONN STRING {envVarKeyName} IS EMPTY.
Solution: If you run terminal INSIDE Rider, go to settings, go to Tools -> Terminal and insert the {envVarKeyName}
environment variable from here.
If you're running from Git Bash OUTSIDE your IDE, go to the .bash_profile file in your home directory, and add the line

export {envVarKeyName}=YOUR CONNECTION STRING HERE

If you use zsh (like Mac users), add the above line in the .zshrc file in your home directory.

Don't forget to close down the terminal after setting environment variable and starting a new one.

Best regards, Alex
🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨🧨
");
        }

        try
        {
            Uri = new Uri(rawConnectionString);
            ProperlyFormattedConnectionString = string.Format(
                "Server={0};Database={1};User Id={2};Password={3};Port={4};Pooling=true;MaxPoolSize=3",
                Uri.Host,
                Uri.AbsolutePath.Trim('/'),
                Uri.UserInfo.Split(':')[0],
                Uri.UserInfo.Split(':')[1],
                Uri.Port > 0 ? Uri.Port : 5432);
            new NpgsqlDataSourceBuilder(ProperlyFormattedConnectionString).Build().OpenConnection().Close();
        }
        catch (Exception e)
        {
            throw new Exception($@"Connection string not found", e);
        }
    }
    
}