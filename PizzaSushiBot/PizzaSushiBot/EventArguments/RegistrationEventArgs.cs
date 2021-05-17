using System;
using PizzaSushiBot.Models;

namespace PizzaSushiBot
{
    sealed class RegistrationEventArgs : EventArgs
    {
        private string _mailBody;

        internal string MailSubject { get; } = "Welcome to PizzaSushiBot!";
        internal string MailBody
        {
            get
            {
                string username = User.GetInstance().Username;
                string password = User.GetInstance().Password;

                _mailBody =
                    $"Thank you for registering at PizzaSushiBot!\n\n" +
                    $"Here are your account credentials:\n" +
                    $"Username: {username}\n" +
                    $"Password: {password}\n\n"; 

                return _mailBody;
            }
        }
    }
}
