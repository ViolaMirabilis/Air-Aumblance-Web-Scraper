using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace air_ambulance_web_scraper.HttpManager
{
    internal static class RefreshFrequency
    {
        internal static void RefreshRequest(HttpRequest httpRequest)
        {
            for (int i = 15; i> 0; i--)
            {
                Console.SetCursorPosition(0, 3);    // sets cursor position
                Console.WriteLine($"Restarting in {i} seconds!\n");
                Thread.Sleep(1000);
            }
        }
    }
}
