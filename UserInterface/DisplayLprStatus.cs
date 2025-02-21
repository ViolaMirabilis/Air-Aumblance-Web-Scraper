using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using air_ambulance_web_scraper.Models;

namespace air_ambulance_web_scraper.UserInterface
{
    internal class DisplayLprStatus
    {
        internal void DisplayStatus(LprStatusNodes lprStatus)
        {
            Console.WriteLine($"Airport: {lprStatus.AirportNode.InnerText}");
            Console.WriteLine($"Coordinates: {lprStatus.LatitudeNode.InnerText}, {lprStatus.LongitudeNode.InnerText}");
            //Console.WriteLine($"Via: {latitudeVia}, {longitudeVia}");
            Console.Write($"Status: ");

            ChangeColor(lprStatus.StatusColorNode.InnerText, lprStatus.StatusNode.InnerText);
        }

        // info available from the HttpRequest
        internal void ChangeColor(string statusColor, string status)
        {
            // simplify this, lol.
            switch (statusColor)
            {
                case "1":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(status);
                    Console.ResetColor();
                    break;
                case "2":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(status);
                    Console.ResetColor();
                    break;
                case "3":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(status);
                    Console.ResetColor();
                    break;
                case "4":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(status);
                    Console.ResetColor();
                    break;
                case "5":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(status);
                    Console.ResetColor();
                    break;
                case "6":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(status);
                    Console.ResetColor();
                    break;
                case "7":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(status);
                    Console.ResetColor();
                    break;
                case "10":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("dyżur zakończony");
                    Console.ResetColor();
                    break;

            }
        }
    }
}
