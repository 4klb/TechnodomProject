using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data.SqlClient;


namespace TechnodomProject.Services
{
    public static class ConfigurationService
    {
        public static IConfiguration Configuration { get; private set; }

        public static void Init()
        {
            if (DbProviderFactories.TryGetFactory("OnlineShopProvider", out DbProviderFactory factory) == false)
            {
                DbProviderFactories.RegisterFactory("OnlineShopProvider", SqlClientFactory.Instance);
            }

            if (Configuration == null)
            {
                var configurationBuilder = new ConfigurationBuilder();
                Configuration = configurationBuilder.AddJsonFile("appSettings.json").Build();

            }
        }
    }
}
