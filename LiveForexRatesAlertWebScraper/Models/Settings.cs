using System;
using System.Collections.Generic;
using System.Text;

namespace LiveForexRatesAlertWebScraper.Models
{
    public class Alert
    {
        public string name { get; set; }
        public string price { get; set; }
        public string direction { get; set; }
    }

    public class Sender
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Email
    {
        public string HostAddress { get; set; }
        public int HostPort { get; set; }
        public Sender Sender { get; set; }
        public string RecipientEmail { get; set; }
    }

    public class Settings
    {
        public List<Alert> alerts { get; set; }
        public Email Email { get; set; }
    }
}
