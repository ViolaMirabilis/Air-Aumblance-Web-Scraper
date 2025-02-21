using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;

namespace air_ambulance_web_scraper
{
    internal static class Constants
    {
        // must be readonly so it is declared as constant and the method is available to be run
        internal static readonly string configPath = Directory.GetCurrentDirectory();
        internal const string lprWebsite = @"https://mapy.geoportal.gov.pl/wss/service/sdi/LPR/get?SERVICE=WMS&request=GetFeatureInfo&version=1.1.1&layers=Bazy_HEMS&styles=&srs=EPSG:2180&bbox=25711.851198035758,148320.20997375305,1041713.8832020999,431954.11057488766&width=1920&height=536&format=image/png&transparent=true&query_layers=Bazy_HEMS&x=900&y=318&INFO_FORMAT=text/xml";
        internal const string googleMapsLink = @"https://www.google.com/maps/place/";
        internal const string compareStatus = "1 - zespól w gotowosci"; // needed for letter by letter comparison

        // XPath queries responsible for airport name, latitude/longitude
        internal const string airportXPath = "//default:Attribute[@Name='AIRPORT']";
        internal const string latitudeXPath = "//default:Attribute[@Name='DESTINATIONLAT']";
        internal const string longitudeXPath = "//default:Attribute[@Name='DESTINATIONLON']";
        // the last two are responsible for inter-hospital transport
        internal const string latitudeViaXPath = "//default:Attribute[@Name='DESTINATIONLATVIA']";
        internal const string longitudeViaXPath = "//default:Attribute[@Name='DESTINATIONLONVIA']";

        // responsible for picking up current status along with a corresponding color
        internal const string statusColorXPath = "//default:Attribute[@Name='STATUS']";     // gets statuses from 1-6, indicating their colour
        internal const string statusXPath = "//default:Attribute[@Name='HEMSSTATUSDESCRIPTION']";
    }
}
