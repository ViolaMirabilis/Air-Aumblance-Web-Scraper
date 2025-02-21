using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace air_ambulance_web_scraper.Models
{
    public class Config
    {
        public List<string> RecipientEmail { get; set; }
        public string SMTP_Server { get; set; }
        public string SMTP_Email { get; set; }
        public string SMTP_Password { get; set; }
        public int SMTP_Port { get; set; }

        // holds the list of the statuses the user wants to be notified about.
        public List<MissionType> Status { get; set; }

        public Config() { }         // parameterless for the XmlReader (idk why, it just requires one like this)

        public Config (List<string> personalEmail, string smtpServer, string smtpEmail, string smtpPassword, int smtpPort, List<MissionType> statuses)
        {
            RecipientEmail = personalEmail;
            SMTP_Server = smtpServer;
            SMTP_Email = smtpEmail;
            SMTP_Password = smtpPassword;
            SMTP_Port = smtpPort;
            Status = statuses;
        }

    }
}
