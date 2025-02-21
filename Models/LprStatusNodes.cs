using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace air_ambulance_web_scraper.Models
{
    internal class LprStatusNodes
    {
        //private XmlDocument xmlDoc;
        public XmlNode AirportNode { get; set; }
        public XmlNode LatitudeNode { get; set; }
        public XmlNode LongitudeNode { get; set; }
        public XmlNode LatitudeViaNode { get; set; }
        public XmlNode LongitudeViaNode { get; set; }
        public XmlNode StatusColorNode { get; set; }      //status 1 - 10, indicates colour
        public XmlNode StatusNode { get; set; }

        // a constructor for all possible statuses, including inter-hospital transports
        public LprStatusNodes(XmlNode airportNode, XmlNode latitudeNode, XmlNode longitudeNode, XmlNode latitudeViaNode, XmlNode longitudeViaNode, XmlNode statusColorNode, XmlNode statusNode)
        {
            AirportNode = airportNode;
            LatitudeNode = latitudeNode;
            LongitudeNode = longitudeNode;
            LatitudeViaNode = latitudeViaNode;
            LongitudeViaNode = longitudeViaNode;
            StatusColorNode = statusColorNode;
            StatusNode = statusNode;
        }

        // a constructor for basic statuses (not including inter-hospital transports)
        public LprStatusNodes(XmlNode airportNode, XmlNode latitudeNode, XmlNode longitudeNode, XmlNode statusColorNode, XmlNode statusNode)
        {
            AirportNode = airportNode;
            LatitudeNode = latitudeNode;
            LongitudeNode = longitudeNode;
            StatusColorNode = statusColorNode;
            StatusNode = statusNode;
        }
    }
}
