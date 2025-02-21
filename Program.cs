using System;
using System.IO;
using System.Collections;
using air_ambulance_web_scraper.Controllers;
using air_ambulance_web_scraper.Models;
using air_ambulance_web_scraper.HttpManager;
using air_ambulance_web_scraper.UserInterface;

namespace air_ambulance_web_scraper
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // CONFIG PART ------------------------------------------------------------------------------------------------
            // initialising a config controller
            ConfigController configController = new ConfigController();
            XmlController xmlController = new XmlController();
            Config newConfig = new Config(); // declaring a config so it can be used later on. It needs a new  Config, because otherwise it wouldn't run if the "if" would be the only instruction that runs

            bool configFileExists = configController.CheckIfConfigExists();     // a bool which verifies whether the file exists or not

            // if config doesn't exists, it runs a method which reads all the information and puts it in a "config" variable.
            // Else, it reads an existing .xml file and puts the data in a new Config object
            if (!configFileExists)
            {
                // filling in config.xml information
                var config = configController.ReadConfigInformation();
                
                //creating a file with this information
                xmlController.CreateConfigFile(config);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"CONFIG CREATED IN {Constants.configPath}");
                Console.ResetColor();
            }
            else
            {
                // testing already existing config
                newConfig = xmlController.ReadConfigFromExistingFile();
            }

            // cannot read it like this

            /* int length = newConfig.Status.Count();

            for (int i = 0; i < length; i++)
            {
                // from string to ENUM
                Console.WriteLine(Enum.Parse(typeof(MissionType), newConfig.Status[i].ToString())); 
            } */

            // END OF CONFIG PART --------------------------------------------------------------------------------------------


            // should be on top
            // also, infinite loop
            Console.Clear();
            while (true)
            {
                HttpRequest httpRequest = new HttpRequest();
                DisplayLprStatus displayStatus = new DisplayLprStatus();
                SendEmail email = new SendEmail();
                RefreshFrequency.RefreshRequest(httpRequest);   // refreshes every 15 seconds

                var lprStatus = await httpRequest.GetSimpleHttpData();        // gets assigned again every 15 seconds with updated data
                displayStatus.DisplayStatus(lprStatus);

                email.SendEmailWithStatus(lprStatus, newConfig);
            }
            



            
            
           




        }
    }
}
