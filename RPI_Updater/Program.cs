using RPI_Updater.Managers;
using RPI_Updater.Models;
using System;
using System.IO;

namespace RPI_Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerManager logger = LoggerManager.Instance;
            logger.Log(LogTag.INFO, "RPI UPDATER");

            logger.Log(LogTag.INFO, "Reading configuration file...");
            JsonFileManager<ConfigurationModel> configuration = new JsonFileManager<ConfigurationModel>("configuration.json");
            string errorCfgMsg = configuration.Initialize();
            if (errorCfgMsg != null)
            {
                logger.Log(LogTag.CRITICAL, "Unable to read configuration file: " + errorCfgMsg);
                return;
            }

            logger.Log(LogTag.INFO, "Configuration file version: " + configuration.Content.Version);
            
            logger.Log(LogTag.INFO, "Reading manifest file...");
            JsonFileManager<ManifestModel> manifest = new JsonFileManager<ManifestModel>("manifest.json");
            string errorMfsMsg = manifest.Initialize();
            if (errorMfsMsg != null)
            {
                //First application starting
                logger.Log(LogTag.CRITICAL, "Unable to read manifest file: " + errorMfsMsg);
                return;
            }

            logger.Log(LogTag.INFO, "Manifest file version: " + manifest.Content.Version);

            logger.Log(LogTag.INFO, "Cheking content directory...");
            if (!Directory.Exists("content"))
            {
                Directory.CreateDirectory("content");
            }

            Console.ReadKey();
        }
    }
}
