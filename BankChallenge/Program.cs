namespace BankChallenge
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Serilog.Sinks.Email;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .ConfigureAppConfiguration((context, config) =>
                    {
                        config
                            .SetBasePath(context.HostingEnvironment.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables();

                        if (args != null)
                        {
                            config.AddCommandLine(args);
                        }
                    })
                        .ConfigureLogging((context, logging) =>
                        {
                            Serilog.ILogger? logger = Log.Logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(context.Configuration)
                            .WriteTo.Email(GetEmailConnectionInfo(context.Configuration), restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
                            .CreateLogger();
                            logging.AddSerilog(logger);
                        })

                    .UseStartup<Startup>();
                });


        /// <summary>
        /// this is a workaround i found to enable email sending for serilog and as well read serilog from json files without 
        /// overriding each other
        /// The documentation does not cover this scenario but i managed to find my way around it
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static EmailConnectionInfo? GetEmailConnectionInfo(IConfiguration configuration)
        {
            if (configuration.GetSection("Mail").Exists())
            {
                return new EmailConnectionInfo()
                {
                    EmailSubject = "Error in BankChallenge",
                    EnableSsl = false,
                    Port = int.Parse(configuration["Mail:Port"]),
                    FromEmail = configuration["Mail:From"],
                    ToEmail = configuration["Mail:To"],
                    MailServer = configuration["Mail:Server"],
                    NetworkCredentials = new NetworkCredential(configuration["Mail:User"], configuration["Mail:Password"]),

                    // To bypass Certificate Validation that gives exception in production.
                    //ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true
                };
            }

            return null;
        }
    }
}
