# Live forex rates alert with WebScraper
This is a .NET Core console application what can notify via Email when certain currency pair reaches the specified alert price.

Quote from [https://docs.microsoft.com/en-us/dotnet/api/system.net.mail.smtpclient?view=netcore-3.0#remarks]

_“We don’t recommend that you use the SmtpClient class for new development because SmtpClient doesn't support many modern protocols. 
Use MailKit or other libraries instead. For more information, see SmtpClient shouldn't be used on GitHub.”_

**Therefore for using you have to own a** [http://sendinblue.com] **account and fill the parameters.** 

Alerts and EmailSending can be set up in appsettings.json:
  - "Sender: Password" is the master password from your [http://sendinblue.com] account /SMTP & API/masterPw
  - There is a validation in case set up currency does not exist
  - Direction set as 
        "+" notifies when the real price is above alert price or "-" notifies when the real price is under alert price
  
Pairs and prices are pulled from [https://www.investing.com/currencies/streaming-forex-rates-majors]
