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
        string apiKeyName = primaryConfig.AppSettings.Settings["ApiKeyName"].Value;

        
        ExeConfigurationFileMap configMap = new ExeConfigurationFileMap { ExeConfigFilename = apiKeyLocation };
        Configuration secondaryConfig = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

        ApiKey = secondaryConfig.AppSettings.Settings[apiKeyName].Value;

    }
}
