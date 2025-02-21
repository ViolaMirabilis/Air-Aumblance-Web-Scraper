using MimeKit;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using MailKit;
using MailKit.Net.Smtp;
using air_ambulance_web_scraper.Models;

namespace air_ambulance_web_scraper.HttpManager
{
    internal class SendEmail
    {
        internal void SendEmailWithStatus(LprStatusNodes lprStatus, Config config)
        {
            // GET RID OF ALL THE "INNER TEXT" AND REPLACE IT BEFORE PUTTING IT IN THE FORMATTING STRINGS
            InternetAddressList emails = new InternetAddressList();
            var email = new MimeMessage();
            string googleMapsLink = $"{Constants.googleMapsLink}{lprStatus.LatitudeNode.InnerText},{lprStatus.LongitudeNode.InnerText}";
            string subject = lprStatus.StatusNode.InnerText;
            string body = $@"<a href=""{googleMapsLink}"">Google Maps</a>";     // a hyperlink (clickable from the email message)

            // EMAIL STRUCTURE ------------------
            email.From.Add(new MailboxAddress("Ratownik 4", config.SMTP_Email));

            // converts strings from the XML into a suitable MailboxAddress format
            foreach (var emailAddress in config.RecipientEmail)
                emails.Add(new MailboxAddress("r4", emailAddress));

            email.To.AddRange(emails);

            email.Subject = $"{subject}";
            // END OF EMAIL STRUCTURE -----------

            if (Helpers.compareStatus != lprStatus.StatusNode.InnerText)
            {
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = body  // the body is the status. It is a string from above.
                };
                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(config.SMTP_Server, config.SMTP_Port, true);

                    // Note: only needed if the SMTP server requires authentication
                    smtp.Authenticate(config.SMTP_Email, config.SMTP_Password);

                    smtp.Send(email);
                    smtp.Disconnect(true);

                    Console.Write($"----- Email sent at ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{DateTime.Now}");
                    Console.ResetColor();
                    Console.Write(" -----");

                    Helpers.compareStatus = lprStatus.StatusNode.InnerText;
                }
            }
        }
    }
}
