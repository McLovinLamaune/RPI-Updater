using RPI_Updater.Managers;
using RPI_Updater.Models;
using System;
using System.IO;
using System.Net;

namespace RPI_Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerManager logger = LoggerManager.Instance;
            ApiFileManager apiFile = ApiFileManager.Instance;

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

            logger.Log(LogTag.INFO, "Cheking directories...");
            if (!Directory.Exists("content"))
            {
                Directory.CreateDirectory("content");
            }
            if (!Directory.Exists("packages"))
            {
                Directory.CreateDirectory("packages");
            }

            logger.Log(LogTag.INFO, "Retreiving last manifest from server...");

            bool manifestDlIsOk = apiFile.DownloadFile("manifest.json", "content/manifest_last.json");
            if(!manifestDlIsOk)
            {
                logger.Log(LogTag.ERROR, "Retreive KO");
                return;
            }            

            JsonFileManager<ManifestModel> manifestLast = new JsonFileManager<ManifestModel>("content/manifest_last.json");
            string errorMfsLastMsg = manifestLast.Initialize();

           
            logger.Log(LogTag.INFO, "Last manifest file version: " + manifestLast.Content.Version);

            if(manifest.Content == manifestLast.Content)
            {
                logger.Log(LogTag.INFO, "Manifest already up to date");
                return;
            }

            logger.Log(LogTag.INFO, "Manifest not up to date");

            foreach(PackageModel package in manifestLast.Content.Packages)
            {
                string packagePath = string.Concat("packages", "/", package.Name);

                if (!Directory.Exists(packagePath))
                {
                    Directory.CreateDirectory(packagePath);
                }
            }

            Console.ReadKey();
        }
    }
}
