namespace rest.Framework.ConfigFramework;

public interface IConfig {
    void Load();
}

public class Config : IConfig
{
    public static IConfiguration? DefaultConfig;
    public string FilePath;
    public Config(string filePath){
        FilePath = filePath;
    }
    public void Load()
    {
        var configBuilder = new ConfigurationBuilder();
        configBuilder.AddJsonFile(FilePath);
        DefaultConfig = configBuilder.Build();
    }
}

