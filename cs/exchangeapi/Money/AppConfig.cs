using System;
using System.Configuration;
using System.IO;
using System.Reflection;



namespace TheSoftwareGorilla.TDD.Money;

public class AppConfig
{
    public string ApiKey { get;}
    public string ExchangeApiStandardUrl { get;}
    public string ExchangeApiPairUrl { get;}

    public AppConfig()
    {
        string exePath = Assembly.GetExecutingAssembly().Location;
        string primaryConfigFile = String.Concat(exePath, ".config");
        ExeConfigurationFileMap primaryConfigMap = new ExeConfigurationFileMap { ExeConfigFilename = primaryConfigFile };
        Configuration primaryConfig = ConfigurationManager.OpenMappedExeConfiguration(primaryConfigMap, ConfigurationUserLevel.None);   

        ExchangeApiPairUrl = primaryConfig.AppSettings.Settings["ExchangeApiPairUrl"].Value;
        ExchangeApiStandardUrl = primaryConfig.AppSettings.Settings["ExchangeApiStandardUrl"].Value;

        string apiKeyLocation = primaryConfig.AppSettings.Settings["ApiKeyLocation"].Value;
        if (apiKeyLocation != null && !File.Exists(apiKeyLocation)) {
            apiKeyLocation = GetSolutionPathForFile(apiKeyLocation);
        }
        if (apiKeyLocation == null) {
            throw new FileNotFoundException($"ApiKeyLocation file ({primaryConfig.AppSettings.Settings["ApiKeyLocation"].Value}) not found");
        }
        string apiKeyName = primaryConfig.AppSettings.Settings["ApiKeyName"].Value;

        
        ExeConfigurationFileMap configMap = new ExeConfigurationFileMap { ExeConfigFilename = apiKeyLocation };
        Configuration secondaryConfig = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

        ApiKey = secondaryConfig.AppSettings.Settings[apiKeyName].Value;

    }
    
    public static string GetSolutionPathForFile(string relativeFileName) {
        string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        DirectoryInfo di = new DirectoryInfo(exePath);
        while (di != null && !File.Exists(Path.Combine(di.FullName, relativeFileName))) {
            di = di.Parent;
        }

        if (di == null) {
            return null;
        }

        return Path.Combine(di.FullName, relativeFileName);
    }

}
