
using Microsoft.Extensions.Configuration;

namespace DAOs;
public class ConnectionString
    {
        public static string? GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
            return config["ConnectionStrings:DefaultConnectionStringDB"];

        }

    }