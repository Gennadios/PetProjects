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
    sealed class Login
    {
        private static byte _attempts = 3;
        private static List<User> _users;

        internal static void Initialize()
        {
            Logger.Info("Initialized Login");
            InitializeUsersList();
            SetCursorPosition(0, 13);
            CheckCredentials();
            ForceReturnIfFailed();
        }

        private static void InitializeUsersList()
        {
            if(_users == null)
            {
                using PizzaSushiContext context = new();
                var userList = context.Users.ToList();

                _users = userList;
            }
        }

        private static bool UserIsValid(string username, string password)
        {
            bool output = false;

            User user = _users
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefault();

            if (user != null)
                output = true;

            return output;
        }

        private static void CheckCredentials()
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

        private static User ValidUser(string username)
        {
            User user = _users
                .Where(u => u.Username == username)
                .FirstOrDefault();

            return user;
        }

        private static void ForceReturnIfFailed()
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

        private static string GetUserName()
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

        private static string GetUserPassword()
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

        private static string InputMaskedPassword()
        {
            string pass = string.Empty;
            ConsoleKey key;

            do
            {
                var keyInfo = ReadKey(true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            return pass;
        }

        private static void LogInAs(User user)
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
