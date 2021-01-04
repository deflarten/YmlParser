using Microsoft.Extensions.DependencyInjection;
using YmlParser.Commands;
using System;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using YmlParser.DataExport;
using YmlParser.Repo;
using YmlParser.Web;
using YmlParser.Parser;

namespace YmlParser
{
    class Program
    {
        private static IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
             .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    loggingBuilder.AddNLog();
                })
            .AddSingleton<IDataExport, CsvDataExport>()
            .AddSingleton<IRepository, Repository>()
            .AddSingleton<IWebProvider, WebProvider>()
            .AddSingleton<IYmlParser, StreamParser>()
            .BuildServiceProvider();

        }

        private static CommandManager ConfigureCommandManager(IServiceProvider serviceProvider)
        {
            var manager = new CommandManager();

            manager.Register("save", new SaveCommand(serviceProvider.GetService<IYmlParser>(),
                                                     serviceProvider.GetService<IWebProvider>(),
                                                     serviceProvider.GetService<IRepository>(),
                                                     serviceProvider.GetService<ILogger<SaveCommand>>()));

            manager.Register("print", new PrintCommand(serviceProvider.GetService<IRepository>(),
                                                       serviceProvider.GetService<IDataExport>(),
                                                       serviceProvider.GetService<ILogger<PrintCommand>>()));

            return manager;
        }

        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            try      
            {
                //GC.TryStartNoGCRegion(100000000);
                var serviceProvider = ConfigureServices();
                var manager = ConfigureCommandManager(serviceProvider);

                CommandResult result = manager.Run(args);

                Console.WriteLine(result.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Остановка программы из-за ошибки");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}
