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
    class Registration
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

        private bool UsernameTaken(string username)
        {
            bool output = false;

            using PizzaSushiContext context = new();
            User user = context.Users.
                Where(u => u.Username == username).
                FirstOrDefault();

            if (user?.Username == username) 
                output = true;

            return output;
        }

        private bool EmailTaken(string email)
        {
            bool output = false;

            using PizzaSushiContext context = new();
            User user = context.Users.
                Where(u => u.Email == email).
                FirstOrDefault();

            if (user?.Email == email)
                output = true;

            return output;
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
                if (EmailTaken(email))
                    WriteLine(emailTakenMessage);
                if (IsValidEmail(email) && !EmailTaken(email))
                    break;
            }
            Logger.Info($"Prompted for user email, got {email}");
            return email;
        }

        private string GetUserName()
        {
            string username;
            while (true)
            {
                Write("Username: ");
                username = ReadLine();

                if (!IsValidName(username))
                    WriteLine(usernameInvalidMessage);
                if (UsernameTaken(username))
                    WriteLine(usernameTakenMessage);
                if (IsValidName(username) && !UsernameTaken(username))
                    break;
            }
            return username;
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
            return address;
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

        private static string InputMaskedPassword()
        {
            string pass = string.Empty;
            ConsoleKey key;

            do
            {
                var keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            return pass;
        }

        protected virtual void OnUserRegistered()
        {
            UserRegistered?.Invoke(this, new RegistrationEventArgs());
            Logger.Info("Invoked UserRegistered event");
        }
    }
}
