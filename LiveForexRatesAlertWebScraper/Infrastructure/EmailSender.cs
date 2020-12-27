using MimeKit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using MailKit.Net.Smtp;
using MailKit;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace LiveForexRatesAlertWebScraper.Infrastructure
{
    public class EmailSender
    {
        private const string V = "Title";
        
        public async void test()
        {
            var sett = Config.Get();

            string mailserviceaddress = Config.Get().GetSection("Email:HostAddress").Value;
            int mailserviceport = 587;
            string senderemail = Config.Get().GetSection("Email:Sender:Email").Value;
            string senderpassword = Config.Get().GetSection("Email:Sender:Password").Value;
            string recipientemail = Config.Get().GetSection("Email:RecipientEmail").Value;

            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress("Web Scraper", "price-alert-noreply@localhost.com"));
            msg.To.Add(new MailboxAddress("Me", recipientemail));

            msg.Subject = "this is subject";
            msg.Body = new TextPart("plain") { Text = "msgBody" };

            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(mailserviceaddress, mailserviceport, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(senderemail, senderpassword);

                    await client.SendAsync(msg);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
    }
}
