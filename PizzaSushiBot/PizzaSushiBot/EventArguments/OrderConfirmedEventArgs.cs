using System;
using PizzaSushiBot.Models;

namespace PizzaSushiBot
{
    sealed class OrderConfirmedEventArgs : EventArgs
    {
        private string _mailBody;

        internal string CurrentTimeString { get => DateTime.Now.ToShortTimeString(); }
        internal string MailSubject { get; } = "PizzaSushiBot - Order Confirmed";
        internal string MailBody 
        { 
            get
            {
                User user = User.GetInstance();
                string orderDetails = user.ShoppingCart.GetPurchasesList();

                if (string.IsNullOrEmpty(user.Username))
                    user.Username = "customer";

                _mailBody =
                    $"Greetings, {user.Username}!\n\n" +
                    $"Your order has been confirmed at {CurrentTimeString} and will be ready soon.\n\n" +
                    $"Order details:\n" +
                    $"{orderDetails}";

                return _mailBody;
            } 
        }
    }
}
