using System;
using CustomLogger;
using PizzaSushiBot.Models;
using static System.Console;
using static PizzaSushiBot.Validator;
using static PizzaSushiBot.SystemStrings;
using static PizzaSushiBot.Graphics.Settings;

namespace PizzaSushiBot.Entities
{
    sealed class OrderConfirmation
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

        private string GetUserEmail()
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

        private long GetUserPhone()
        {
            string phone;
            while (true)
            {
                Write("Phone no.: +7");
                phone = ReadLine();

                if (!IsValidPhoneNumber(phone))
                    WriteLine(phoneInvalidMessage);
                else
                    break;
            }
            Logger.Info($"Prompted for user phone, got {phone}");
            return Convert.ToInt64(phone);
        }

        private string GetUserAddress()
        {
            
            string address;
            while (true)
            {
                Write("Address: ");
                address = ReadLine();

                if (!IsValidAddress(address))
                    WriteLine(addressInvalidMessage);
                else
                    break;
            }
            Logger.Info($"Prompted for user address, got {address}");
            return address;
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
