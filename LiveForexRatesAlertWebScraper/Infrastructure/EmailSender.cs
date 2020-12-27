using MimeKit;
using System;
using MailKit.Net.Smtp;

namespace LiveForexRatesAlertWebScraper.Infrastructure
{
    public class EmailSender
    {
        private string _msgBody;
            
        public EmailSender(string msgBody)
        {
            _msgBody = msgBody;
        }
        
        public async void Send()
        {
            string mailserviceaddress = Config.Get().Email.HostAddress;
            int mailserviceport = Config.Get().Email.HostPort;
            string senderemail = Config.Get().Email.Sender.Email;
            string senderpassword = Config.Get().Email.Sender.Password;
            string recipientemail = Config.Get().Email.RecipientEmail;

            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress("Currency Alert", "price-alert-noreply@localhost.com"));
            msg.To.Add(new MailboxAddress("Me", recipientemail));

            msg.Subject = "This is a price alert!";
            msg.Body = new TextPart("plain") { Text = _msgBody };

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
