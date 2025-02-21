using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using air_ambulance_web_scraper.Models;

namespace air_ambulance_web_scraper.Controllers
{
    internal class ConfigController
    {
        internal bool CheckIfConfigExists()
        {
            // checks all the files in the folder
            string[] filesInFolder = Directory.GetFiles(Constants.configPath);

            foreach (string file in filesInFolder)
            {
                // returns a name of the file without the directory (e.g. Program.cs)
                string fileName = Path.GetFileName(file);

                if (fileName.Equals("config.xml"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("config found");
                    Console.ResetColor();
                    return true;
                }
            }

            return false;

        }

        internal Config ReadConfigInformation()
        {
            // holds up to 3 emails
            List<string> emails = new List<string>();
            List<MissionType> statuses = new List<MissionType>();
            string smtpEmail, smtpPassword, smtpServer;
            int smtpPort;

            // recipient email input if config doesnt exist
            if (!CheckIfConfigExists())
            {
                Console.WriteLine("----- Configuration file -----");
                Console.WriteLine("Send emails to: ");
                
                for (int i = 0; i < 3; i++)
                {
                    Console.Write($"Email #{i+1}: ");
                    string? email = Console.ReadLine();
                    if (!string.IsNullOrEmpty(email) && email.Contains('@'))
                    {
                        emails.Add(email);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Email(s) added!");
                        Console.ResetColor();
                    }
                    else
                    {
                        break;
                    }

                }
            }
            
            // SMTP email input
            while (true)
            {
                Console.Write("Your SMTP email: ");
                smtpEmail = Console.ReadLine();

                if (!string.IsNullOrEmpty(smtpEmail) && smtpEmail.Contains('@'))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("SMTP Email added!");
                    Console.ResetColor();
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Try again.");
                    Console.ResetColor();
                    continue;
                }
            }

            // SMTP Password input
            while (true)
            {
                Console.Write("Your SMTP password: ");
                smtpPassword = Console.ReadLine();

                if (!string.IsNullOrEmpty(smtpPassword))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("SMTP Password added!");
                    Console.ResetColor();
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Try again.");
                    Console.ResetColor();
                    continue;
                }
            }

            // SMTP Server input
            while (true)
            {
                Console.Write("Your SMTP server: ");
                smtpServer = Console.ReadLine();

                if (!string.IsNullOrEmpty(smtpServer))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("SMTP server added!");
                    Console.ResetColor();
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Try again.");
                    Console.ResetColor();
                    continue;
                }
            }

            // SMTP Port input
            while (true)
            {
                Console.Write("SMTP Port:");
                string port = Console.ReadLine();

                // out _ means to not assign it anywhere.
                if (int.TryParse(port, out smtpPort))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("SMTP Port added!");
                    Console.ResetColor();
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Try again.");
                    Console.ResetColor();
                    continue;
                }
            }

            // adding statuses to be delivered via email
            Console.WriteLine("----- Available Statuses -----\n" +
                    "1. Dyzur zakonczony\n" +
                    "2. Zespol w gotowosci\n" +
                    "3. Lot po pacjenta\n" +
                    "4. Powrot na radiu\n" +
                    "5. Dyzur zawieszony\n");

            while (true)
            {
                // CHECK THIS SECTION LOL

                Console.Write("Status: ");
                string statusInput = Console.ReadLine();
                int status;

                // checks if the input is integer. If it's null or empty or less than 1 statuses have been checked - it loops again.
                if (!int.TryParse(statusInput, out status))
                {
                    // Console.WriteLine("Invalid input.");
                    // continue;
                    break;
                }
                else if ((string.IsNullOrEmpty(statusInput) && (statuses.Count > 1)))
                {
                    continue;
                }

                if (!statuses.Contains((MissionType)status))
                {
                    // because index starts at 0
                    status = status - 1;
                    statuses.Add((MissionType)status);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("STATUS added!");
                    Console.ResetColor();
                    continue;
                }
                else if (statuses.Contains((MissionType)status))
                {
                    Console.WriteLine("This status is already checked! Choose another one!");
                    continue;
                }
                else if (status == 0)
                {
                    break;
                }
            }

            // displays potential configuration and asks for confirmation
            Console.Clear();
            Console.WriteLine("---- Your Configuration ----");
            Console.WriteLine("Recipients: ");

            for (int i = 0; i < emails.Count; i++)
            {
                Console.WriteLine($"{i+1}. {emails[i]}");
            }

            Console.WriteLine($"SMTP E-mail: {smtpEmail}");
            Console.WriteLine($"SMTP Port: {smtpPort}");

            // displays a status of the next index. gotta fix.
            Console.WriteLine("Selected statuses: ");
            for (int i = 0; i < statuses.Count; i++)
            {
                // creating a "mission" variable of MissionType[enum] type and then casting the INTEGER (index) to the enum.
                //MissionType mission = (MissionType)statuses[i];
                Console.WriteLine($"{i+1}. {statuses[i].ToString()}");
            }

            //Console.WriteLine("Confirm your config? Yes/No");

            Config config = new Config(emails, smtpServer, smtpEmail, smtpPassword, smtpPort, statuses);
            return config;
        }

    }

}
