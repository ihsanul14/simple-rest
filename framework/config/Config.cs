namespace simple_rest.framework.config;

public class Config
{
    public static IConfiguration? DefaultConfig;
    public static void Load(string filePath)
    {
        var configBuilder = new ConfigurationBuilder();
        configBuilder.AddJsonFile(filePath);
        DefaultConfig = configBuilder.Build();
    }
}

