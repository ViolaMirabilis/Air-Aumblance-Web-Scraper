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

            using (FileStream stream = File.OpenWrite($@"{Constants.configPath}\config.xml"))
            {
                serializer.Serialize(stream, config);
            }

        }

        internal Config ReadConfigFromExistingFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Config));

            Config config;

            using (Stream reader = new FileStream($@"{Constants.configPath}\config.xml", FileMode.Open))
            {
                config = (Config)serializer.Deserialize(reader);
            }

            return config;
            
        }

    }
}
