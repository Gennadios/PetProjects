using System;
using PizzaSushiBot.Models;

namespace PizzaSushiBot
{
    sealed class OrderPreparedEventArgs : EventArgs
    {
        private string _mailBody;

        internal DateTime DeliveryTime { get => DateTime.Now.AddHours(1); }
        internal string MailSubject { get; } = "PizzaSushiBot - Order is on its way!";
        internal string MailBody 
        { 
            get
            {
                User user = User.GetInstance();

                if (string.IsNullOrEmpty(user.Username))
                    user.Username = "customer";

                _mailBody =
                    $"Dear {user.Username}, your order is ready!\n\n" +
                    $"The courier will arrive at {DeliveryTime}.\n" +
                    $"Courier phone number: +79166772076";

                return _mailBody;
            }    
        }
    }
}
