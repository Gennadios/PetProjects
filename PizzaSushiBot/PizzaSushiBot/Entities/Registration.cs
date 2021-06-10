using System;
using System.Linq;
using CustomLogger;
using PizzaSushiBot.Data;
using PizzaSushiBot.Models;
using static System.Console;
using System.Collections.Generic;
using static PizzaSushiBot.Validator;
using static PizzaSushiBot.SystemStrings;

namespace PizzaSushiBot.Entities
{
    class Registration : FormBase
    {
        private static Registration _registration;
        private static List<User> _users;

        internal EventHandler<RegistrationEventArgs> UserRegistered;

        private Registration() { }

        internal static Registration GetInstance()
        {
            if (_registration == null)
                _registration = new Registration();

            return _registration;
        }

        internal void Initialize()
        {
            InitializeUsersList();
            SetCursorPosition(0, 13);
            string username = GetUserName();
            string email = GetUserEmail();
            long phone = GetUserPhone();
            string address = GetUserAddress();
            string password = GetUserPassword();

            User newUser = User.GetInstance();
            InitializeUserProperties(newUser, username, email, phone, address, password);
            AddUserToDatabase(newUser);
            OnUserRegistered();

            WriteLine($"\nRegistration successful! Logged in as {newUser.Username}");
            WriteLine(pressKeyPrompt);
            ReadKey(true);
        }

        private void InitializeUsersList()
        {
            if (_users == null)
            {
                using PizzaSushiContext context = new();
                var userList = context.Users.ToList();

                _users = userList;
            }
        }

        private void InitializeUserProperties(User user, string username, string email, long phone, string address, string password)
        {
            user.Username = username;
            user.Email = email;
            user.Phone = phone;
            user.Address = address;
            user.Password = password;
        }

        private void AddUserToDatabase(User user)
        {
            using PizzaSushiContext context = new();
            context.Add(user);
            context.SaveChanges();
            Logger.Info($"Added {user} to Database");
        }

        private string GetUserPassword()
        {
            string pwd;
            while (true)
            {
                Write("Password: ");
                pwd = InputMaskedPassword();

                if (!IsValidPassword(pwd))
                    WriteLine(passwordInvalidMessage);
                else
                    break;
            }
            return pwd;
        }

        protected virtual void OnUserRegistered()
        {
            UserRegistered?.Invoke(this, new RegistrationEventArgs());
            Logger.Info("Invoked UserRegistered event");
        }
    }
}
