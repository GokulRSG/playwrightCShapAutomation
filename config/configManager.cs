using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace playwrightWithCFramework.config
{
    public static class ConfigManager
    {
        private static IConfiguration _configuration;

        // Static constructor to initialize the configuration by loading settings from the appsetting.json file.
        static ConfigManager()
        {
            var basePath = AppContext.BaseDirectory;
            _configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsetting.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static string GetEnvironment => _configuration["Environment"];
        public static string GetBaseUrl => _configuration["BaseUrl"];
        public static string GetBrowser => _configuration["Browser"];
        public static int GetTimeout => int.Parse(_configuration["Timeout"]);
        public static bool Headless => bool.Parse(_configuration["Headless"] ?? "false");
    }
}