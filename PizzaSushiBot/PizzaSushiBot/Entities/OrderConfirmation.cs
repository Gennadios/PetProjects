using System;
using CustomLogger;
using PizzaSushiBot.Models;
using static System.Console;
using static PizzaSushiBot.Validator;
using static PizzaSushiBot.SystemStrings;
using static PizzaSushiBot.Graphics.Settings;

namespace PizzaSushiBot.Entities
{
    sealed class OrderConfirmation : FormBase
    {
        private static OrderConfirmation _instance;

        internal EventHandler<OrderConfirmedEventArgs> OrderConfirmed;
        internal EventHandler<OrderPreparedEventArgs> OrderPrepared;

        private OrderConfirmation() { }

        internal static OrderConfirmation GetInstance()
        {
            if (_instance == null)
                _instance = new OrderConfirmation();

            return _instance;
        }

        internal void Initialize()
        {
            Logger.Info("Initialized OrderConfirmation.");
            Clear();
            Header.Draw();
            WriteLine(new string('-', MenuWidth));
            User user = User.GetInstance();
            if (string.IsNullOrEmpty(user.Address))
                RequestDeliveryInfo();

            DisplayConfirmationInfo();
            OnOrderConfirmed();
            OnOrderPrepared();
            SayGoodBye();
        }

        private void RequestDeliveryInfo()
        {
            deliveryAddressPrompt.AlignCenterAndPrint(MenuWidth);

            string address = GetUserAddress();
            long phone = GetUserPhone();
            string email = GetUserEmail();

            User user = User.GetInstance();
            user.Address = address;
            user.Email = email;
            user.Phone = phone;
        }

        private void DisplayConfirmationInfo()
        {
            orderConfirmedMessage.AlignCenterAndPrint(MenuWidth);
            
            User user = User.GetInstance();
            string emailConfirmation =
                $"Order confirmation has been sent to {user.Email}";

            emailConfirmation.AlignCenterAndPrint(MenuWidth);
        }

        protected override string GetUserEmail()
        {
            string email;
            while (true)
            {
                Write("Email: ");
                email = ReadLine();

                if (!IsValidEmail(email))
                    WriteLine(emailInvalidMessage);
                else
                    break;
            }
            Logger.Info($"Prompted for user email, got {email}");
            return email;
        }

        private void SayGoodBye()
        {
            orderIsReadyMessage.AlignCenterAndPrint(MenuWidth);
            ReadKey(true);
            Logger.Info("Session ended.");
        }

        internal void OnOrderConfirmed()
        {
            OrderConfirmed?.Invoke(this, new OrderConfirmedEventArgs());
            Logger.Info("Invoked OrderConfirmed event");
        }

        internal void OnOrderPrepared()
        {
            OrderPrepared?.Invoke(this, new OrderPreparedEventArgs());
            Logger.Info("Invoked OrderConfirmed event");
        }
    }
}
