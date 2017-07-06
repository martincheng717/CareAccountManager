using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Gdot.Care.Common.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gdot.Care.Common.Utilities
{
    [ExcludeFromCodeCoverage]
    public sealed class ConfigManager
    {
        private List<ExternalApi> _apiEndPoints;
        private static NameValueCollection _appSettings;
        private dynamic _config;
        private static readonly Lazy<ConfigManager> Lazy = new Lazy<ConfigManager>(() => new ConfigManager());

        public static ConfigManager Instance => Lazy.Value;

        public dynamic Configuration
        {
            get
            {
                if (_config == null)
                {
                    var configFile = GetAppSetting("metadata");
                    if (!string.IsNullOrEmpty(configFile))
                    {
                        var filePath = Utility.GetFullpath(configFile);
                        var json = File.ReadAllText(filePath);
                        _config = JsonConvert.DeserializeObject<dynamic>(json);
                    }
                }
                return _config;
            }
        }

        public string GetApiEndpoint(string name)
        {
            if (_apiEndPoints == null)
            {
                _apiEndPoints = ((JArray) Configuration.ExternalApi).ToObject<List<ExternalApi>>();
            }
            var apiInfo = _apiEndPoints?.FirstOrDefault(a => a.Name == name);
            return apiInfo?.Url;
        }

        public static string GetAppSetting(string name)
        {
            var apiEndpoint = string.Empty;
            if (_appSettings == null)
            {
                _appSettings = ConfigurationManager.AppSettings;
            }
            if (_appSettings?.Count > 0)
            {
                apiEndpoint = _appSettings[name];
            }
            return apiEndpoint;
        }

        public FileInfo GetLog4NetConfig(string fileName = "log4net.xml")
        {
            var filePath = Utility.GetFullpath(fileName);
            return new FileInfo(filePath);
        }

        private readonly string _defaultConnectionName = "Default";
        public string DefaultConnection => GetConnectionString(_defaultConnectionName);

        public string GetConnectionString(string connectionName)
        {
            var connectionString = string.Empty;
            var connection = ConfigurationManager.ConnectionStrings[connectionName];
            if (!string.IsNullOrEmpty(connection?.ConnectionString))
            {
                connectionString = connection.ConnectionString;
            }
            else
            {
                //try to read it from other section or from machine.config (for backward compatibility)
                var config = ConfigurationManager.GetSection($"DataAccess/Connections/{connectionName}") as NameValueCollection;
                if (config != null)
                {
                    connectionString = $"Data Source={config["Server"]};Initial Catalog={config["Database"]};Integrated Security=True";
#if DEBUG
                    if ((bool) Configuration.EnableDBStandardSecurityOnDebugMode)
                    {
                        var loginInfo = config["Hashvar"].Split('{');
                        connectionString =
                            $"Data Source={config["Server"]};Initial Catalog={config["Database"]};Integrated Security=false;User ID={loginInfo[0]};Password={loginInfo[1]}";
                    }
#endif
                    return connectionString;
                }
                
            }
            return connectionString;
        }
    }
}
