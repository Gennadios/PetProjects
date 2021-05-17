using System.Net;
using CustomLogger;
using System.Net.Mail;
using System.Threading;
using PizzaSushiBot.Models;
using PizzaSushiBot.Entities;

namespace PizzaSushiBot.Services
{
    sealed class MailService
    {
        private static MailService _instance;

        private readonly string _botAddress = "pizzasushibot@gmail.com";
        private readonly string _mailPwd = "ITacademyproject0521";
        private string _userAddress;

        internal string UserAddress 
        { 
            get
            {
                _userAddress = User.GetInstance().Email;
                return _userAddress;
            } 
        }
        internal string Subject { get; set; }
        internal string Body { get; set; }

        private MailService() { }

        internal static MailService GetInstance()
        {
            Logger.Info("Instantiated MailService");
            if (_instance == null)
            {
                _instance = new MailService();
                Registration.GetInstance().UserRegistered += _instance.OnUserRegistered;
                OrderConfirmation.GetInstance().OrderConfirmed += _instance.OnOrderConfirmed;
                OrderConfirmation.GetInstance().OrderPrepared += _instance.OnOrderPrepared;
            }

            return _instance;
        }

        internal void SendMail(string subject, string body)
        {
            MailAddress sender = new(_botAddress);
            MailAddress recipient = new(UserAddress);

            using MailMessage mail = new(sender, recipient);
            mail.Subject = subject;
            mail.Body = body;

            using SmtpClient smtpClient = new();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(sender.Address, _mailPwd);

            smtpClient.Send(mail);
            Logger.Info("Sent email.");
        }

        internal void OnUserRegistered(object sender, RegistrationEventArgs e) => SendMail(e.MailSubject, e.MailBody);
        internal void OnOrderConfirmed(object sender, OrderConfirmedEventArgs e) => SendMail(e.MailSubject, e.MailBody);
        internal void OnOrderPrepared(object sender, OrderPreparedEventArgs e) 
        {
            Thread.Sleep(15000);
            SendMail(e.MailSubject, e.MailBody);
        } 
    }
}
