using RPI_Updater.Managers;
using RPI_Updater.Models;
using System;

namespace RPI_Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerManager logger = LoggerManager.Instance;
            logger.Log(LogTag.INFO, "RPI UPDATER");
            logger.Log(LogTag.INFO, "Reading configuration file...");
            JsonFileManager<ConfigurationModel> configuration = new JsonFileManager<ConfigurationModel>("configurtion.json");
            string isConfigOk = configuration.Initialize();
            if (isConfigOk != null)
            {
                logger.Log(LogTag.CRITICAL, "Unable to read configuration file: " + isConfigOk);
                return;
            }

            logger.Log(LogTag.INFO, "Version: " + configuration.Content.Version);


            Console.ReadKey();
        }
    }
}
