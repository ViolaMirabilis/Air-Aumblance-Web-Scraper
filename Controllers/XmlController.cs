using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using air_ambulance_web_scraper.Models;
using air_ambulance_web_scraper.Controllers;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Reflection.Metadata.Ecma335;
using System.ComponentModel.DataAnnotations;

namespace air_ambulance_web_scraper.Controllers
{
    internal class XmlController
    {
        

        internal void CreateConfigFile(Config config)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            //FileStream stream = File.OpenWrite(Helpers.configPath);

            using (FileStream stream = File.OpenWrite($@"{Constants.configPath}\config.xml"))
            {
                serializer.Serialize(stream, config);
            }

            /* serializer.Serialize(stream, new Config())
            {
                RecipientEmail = config.RecipientEmail,
                SMTP_Email = config.SMTP_Email,
                SMTP_Port = config.SMTP_Port,
                Status = config.Status
            }); */

        }

        internal Config ReadConfigFromExistingFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Config));

            Config config;

            using (Stream reader = new FileStream($@"{Constants.configPath}\config.xml", FileMode.Open))
            {
                config = (Config)serializer.Deserialize(reader);
            }

            // DEBUG MODE (testing if it prints all the data correctly) -----------------------------------------------------

            // status doesn't work even with ToString(); It only works if a specified index is provided
            //Console.WriteLine(config.Status.ToString());
            //Console.WriteLine(config.Status);
            //Console.WriteLine(config.RecipientEmail[0].ToString());

            foreach (var status in config.Status)
            {
                // adds a status one by one (by reading from the XML)
                //config.Status.Add(status);
                Console.WriteLine(status);
            }

            foreach (var email in config.RecipientEmail)
            {
                // adds emails one by one (by reading from the XML)
                //config.RecipientEmail.Add(email);
                Console.WriteLine(email);
            }
            
            Console.WriteLine(config.SMTP_Email);
            Console.WriteLine(config.SMTP_Port);

            // END OF DEBUG MOGE ------------------------------------------------------------------------------------------

            return config;
            
        }

    }
}
