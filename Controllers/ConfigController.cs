using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
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
            List<string> emails = ValidateMultipleEmails("Email: ", "Invalid Input");
            List<MissionType> statuses = ValidateStatuses("Status: ", "Invalid Input");
            string smtpEmail = ValidateEmail("SMTP Email: ", "Invalid Input");
            string smtpPassword = ValidateInput("SMTP Password: ", "Invalid Input"); ;
            string smtpServer = ValidateInput("SMTP Server: ", "Invalid Input"); ;
            int smtpPort = ValidateInteger("SMTP Port:", "Invalid Input");

            Config config = new Config(emails, smtpServer, smtpEmail, smtpPassword, smtpPort, statuses);
            return config;

            DisplayConfig(config);
            
        }

        private string ValidateInput(string message, string invalidInput)
        {
            while (true)
            {
                Console.Write($"{message}: ");
                string input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Added successfully!");
                    Console.ResetColor();
                    return input;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(invalidInput);
                Console.ResetColor();
            }
        }
        private string ValidateEmail(string message, string invalidInput)
        {
            while (true)
            {
                Console.Write($"{message}: ");
                string input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input) && input.Contains('@'))
                {
                    return input;
                }
                else if (string.IsNullOrEmpty(input))
                {
                    return null;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(invalidInput);
                Console.ResetColor();
            }
        }
        private string ValidateEmail(string invalidInput)
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input) && input.Contains('@'))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Added successfully!");
                    Console.ResetColor();
                    return input;
                }
                else if (string.IsNullOrEmpty(input))
                {
                    return null;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(invalidInput);
                Console.ResetColor();
            }
        }
        private int ValidateInteger(string message, string invalidInput)
        {
            while (true)
            {
                Console.Write($"{message}: ");
                string input = Console.ReadLine();
                int numeral;
                if (!string.IsNullOrEmpty(input))
                {
                    if (int.TryParse(input, out numeral))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Added successfully!");
                        Console.ResetColor();
                        return numeral;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(invalidInput);
                    Console.ResetColor();
                }
                
            }
        }
        private List<string> ValidateMultipleEmails(string message, string invalidInput)
        {
            List<string> emails = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                Console.Write(message);
                string input = ValidateEmail(invalidInput);
                if (!string.IsNullOrEmpty(input))
                {
                    emails.Add(input);
                }
                else
                {
                    break;
                }
            }
            return emails;
        }
        private List<MissionType> ValidateStatuses(string message, string invalidInput)
        {
            Console.WriteLine("----- Available Statuses -----\n" +
                    "1. Dyzur zakonczony\n" +
                    "2. Zespol w gotowosci\n" +
                    "3. Lot po pacjenta\n" +
                    "4. Powrot na radiu\n" +
                    "5. Dyzur zawieszony\n");

            List<MissionType> statuses = new List<MissionType>();
            while (true)
            {
                int statusInput = ValidateInteger(message, invalidInput);

                if (statusInput == 0)
                {
                    break;
                }

                if (!statuses.Contains((MissionType)statusInput))
                {
                    // because index starts at 0
                    statusInput = statusInput - 1;
                    statuses.Add((MissionType)statusInput);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("STATUS added!");
                    Console.ResetColor();
                }
                else if (statuses.Contains((MissionType)statusInput))
                {
                    Console.WriteLine("This status is already checked! Choose another one!");
                }
            }
            return statuses;
        }
        private void DisplayConfig(Config config)
        {
            Console.WriteLine("Press ANY key to continue...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("---- Your Configuration ----");
            Console.WriteLine("Recipients: ");

            for (int i = 0; i < config.RecipientEmail.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {config.RecipientEmail[i]}");
            }
            Console.WriteLine($"SMTP E-mail: {config.SMTP_Email}");
            Console.WriteLine($"SMTP Port: {config.SMTP_Port}");

            // displays a status of the next index. gotta fix.
            Console.WriteLine("Selected statuses: ");
            for (int i = 0; i < config.Status.Count; i++)
            {
                // creating a "mission" variable of MissionType[enum] type and then casting the INTEGER (index) to the enum.
                //MissionType mission = (MissionType)statuses[i];
                Console.WriteLine($"{i + 1}. {config.Status[i].ToString()}");
            }
        }
        
    }

}
