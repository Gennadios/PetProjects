using System;
using System.Linq;
using CustomLogger;
using PizzaSushiBot.Data;
using PizzaSushiBot.Models;
using static System.Console;
using System.Collections.Generic;
using static PizzaSushiBot.Validator;
using static PizzaSushiBot.SystemStrings;
using static PizzaSushiBot.Graphics.Settings;

namespace PizzaSushiBot.Entities.Menus
{
    sealed class Login : FormBase
    {
        private static byte _attempts = 3;
        private List<User> _users;

        internal void Initialize()
        {
            Logger.Info("Initialized Login");
            InitializeUsersList();
            SetCursorPosition(0, 13);
            CheckCredentials();
            ForceReturnIfFailed();
        }

        private void InitializeUsersList()
        {
            if(_users == null)
            {
                using PizzaSushiContext context = new();
                var userList = context.Users.ToList();

                _users = userList;
            }
        }

        private bool UserIsValid(string username, string password)
        {
            bool output = false;

            User user = _users
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefault();

            if (user != null)
                output = true;

            return output;
        }

        private void CheckCredentials()
        {
            while (true)
            {
                if (_attempts == 0) break;
                string username = GetUserName();
                string password = GetUserPassword();
                if (UserIsValid(username, password))
                {
                    LogInAs(ValidUser(username));
                    WriteLine($"\nLogin successful! Logged in as {username}.");
                    WriteLine(pressKeyPrompt);
                    ReadKey(true);
                    break;
                }
                else
                {
                    _attempts--;
                    Write("\n" + loginFailedMessage);
                    WriteLine($" {_attempts} attempts left.");
                    Logger.Info($"User failed login attempt #{3 - _attempts}");
                }
            }
        }

        private User ValidUser(string username)
        {
            User user = _users
                .Where(u => u.Username == username)
                .FirstOrDefault();

            return user;
        }

        private void ForceReturnIfFailed()
        {
            if (_attempts == 0)
            {
                WriteLine(new string('-', MenuWidth));
                guestOrRegisterMessage.AlignCenterAndPrint(MenuWidth);
                ReadKey(true);
                _attempts = 0;
                Logger.Info($"Force returned user to WelcomeMenu");
                new WelcomeMenu().Display();
            }
        }

        protected override string GetUserName()
        {
            string username;
            while (true)
            {
                Write("Username: ");
                username = ReadLine();

                if (!IsValidName(username))
                    WriteLine(usernameInvalidMessage);
                if (IsValidName(username))
                    break;
            }
            Logger.Info($"Prompted for username, got {username}");
            return username;
        }

        private string GetUserPassword()
        {
            string pwd;
            while (true)
            {
                Write("Password: ");
                pwd = InputMaskedPassword();

                if (!IsValidPassword(pwd))
                    WriteLine("\n" + passwordInvalidMessage);
                else
                    break;
            }
            Logger.Info($"Prompted for password");
            return pwd;
        }

        private void LogInAs(User user)
        {
            User currentUser = User.GetInstance();

            currentUser.Id = user.Id;
            currentUser.Username = user.Username;
            currentUser.Email = user.Email;
            currentUser.Phone = user.Phone;
            currentUser.Address = user.Address;
            currentUser.Password = user.Password;

            Logger.Info($"Logged in as {user}");
        }
    }
}
