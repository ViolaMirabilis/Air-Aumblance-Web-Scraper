using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using air_ambulance_web_scraper.Models;

namespace air_ambulance_web_scraper.HttpManager
{
    internal class HttpRequest
    {
        string airport = "", latitude = "", longitude = "", latitudeVia = "", longitudeVia = "", statusColor = "", status = "";
        // AllHttpData means all possible statuses
        // GetSimpleHttpData means the exception of inter-hospital transports
        internal async Task<LprStatusNodes> GetAllHttpData()
        {
            LprStatusNodes statusNodes = null;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // sends a "get" request to the LPR website
                    HttpResponseMessage response = await client.GetAsync(Constants.lprWebsite);

                    // informs whether got a response or not
                    response.EnsureSuccessStatusCode();

                    // reads the XML content from the web as a string
                    string xmlContent = await response.Content.ReadAsStringAsync();

                    // forks all the data into one file
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlContent);

                    // XmlNamespaceManager handles the namespaces in the XML
                    XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);

                    // Add the necessary namespaces (needed for proper extraction I believe)
                    namespaceManager.AddNamespace("gml", "http://www.opengis.net/gml");
                    namespaceManager.AddNamespace("default", "http://www.intergraph.com/geomedia/gml");

                    // nodes for all potential statuses 
                    XmlNode airportNode = xmlDoc.SelectSingleNode(Constants.airportXPath, namespaceManager);
                    XmlNode latitudeNode = xmlDoc.SelectSingleNode(Constants.latitudeXPath, namespaceManager);
                    XmlNode longitudeNode = xmlDoc.SelectSingleNode(Constants.longitudeXPath, namespaceManager);
                    XmlNode latitudeViaNode = xmlDoc.SelectSingleNode(Constants.latitudeViaXPath, namespaceManager);
                    XmlNode longitudeViaNode = xmlDoc.SelectSingleNode(Constants.longitudeViaXPath, namespaceManager);
                    XmlNode statusColorNode = xmlDoc.SelectSingleNode(Constants.statusColorXPath, namespaceManager);      //status 1 - 10, indicates colour
                    XmlNode statusNode = xmlDoc.SelectSingleNode(Constants.statusXPath, namespaceManager);

                    // Get the values or default to "N/A" if not found
                    airport = airportNode?.InnerText ?? "N/A";
                    latitude = latitudeNode?.InnerText ?? "N/A";
                    longitude = longitudeNode?.InnerText ?? "N/A";
                    latitudeVia = latitudeViaNode?.InnerText ?? "N/A";
                    longitudeVia = longitudeViaNode?.InnerText ?? "N/A";
                    statusColor = statusColorNode?.InnerText ?? "N/A";
                    status = statusNode?.InnerText ?? "N/A";

                    statusNodes = new LprStatusNodes(airportNode, latitudeNode, longitudeNode, latitudeViaNode, longitudeViaNode, statusColorNode, statusNode);
                }
                catch
                {

                }

                return statusNodes;
            }

        }
        internal async Task<LprStatusNodes> GetSimpleHttpData()     // needs to be a Task<YOUR_TYPE> to be able to return a value.
        {
            LprStatusNodes statusNodes = null;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // sends a "get" request to the LPR website
                    HttpResponseMessage response = await client.GetAsync(Constants.lprWebsite);

                    // informs whether got a response or not
                    response.EnsureSuccessStatusCode();

                    // reads the XML content from the web as a string
                    string xmlContent = await response.Content.ReadAsStringAsync();

                    // forks all the data into one file
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlContent);

                    // XmlNamespaceManager handles the namespaces in the XML
                    XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);

                    // Add the necessary namespaces (needed for proper extraction I believe)
                    namespaceManager.AddNamespace("gml", "http://www.opengis.net/gml");
                    namespaceManager.AddNamespace("default", "http://www.intergraph.com/geomedia/gml");

                    // nodes for all potential statuses 
                    XmlNode airportNode = xmlDoc.SelectSingleNode(Constants.airportXPath, namespaceManager);
                    XmlNode latitudeNode = xmlDoc.SelectSingleNode(Constants.latitudeXPath, namespaceManager);
                    XmlNode longitudeNode = xmlDoc.SelectSingleNode(Constants.longitudeXPath, namespaceManager);
                    XmlNode statusColorNode = xmlDoc.SelectSingleNode(Constants.statusColorXPath, namespaceManager);      //status 1 - 10, indicates colour
                    XmlNode statusNode = xmlDoc.SelectSingleNode(Constants.statusXPath, namespaceManager);

                    // Get the values or default to "N/A" if not found
                    airport = airportNode.InnerText ?? "N/A";
                    latitude = latitudeNode.InnerText ?? "N/A";
                    longitude = longitudeNode.InnerText ?? "N/A";
                    statusColor = statusColorNode.InnerText ?? "N/A";
                    status = statusNode.InnerText ?? "N/A";

                    statusNodes = new LprStatusNodes(airportNode, latitudeNode, longitudeNode, statusColorNode, statusNode);

                }
                catch
                {
                    
                }
                return statusNodes;

            }

        }


    }
}
