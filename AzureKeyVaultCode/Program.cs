using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureKeyVaultCode
{
    public class Program
    {

        // --------------------Need to Install Namespace from Nuget Package --------------------
        // Azure.Extensions.AspNetCore.Configuration.Secrets
        // Azure.Identity
        // Azure.Security.KeyValut.Secrets
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                var builtconfigruation = config.Build();
                string kvURL = builtconfigruation["KVurl"];
                string tenantId = builtconfigruation["TenantId"];
                string clientId = builtconfigruation["ClientId"];
                string clientSecrete = builtconfigruation["ClientSecretedId"];
                var Credential = new ClientSecretCredential(tenantId, clientId, clientSecrete);
                var client = new SecretClient(new Uri(kvURL), Credential);
                config.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
            })
            .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
