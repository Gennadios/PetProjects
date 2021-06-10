using System;
using System.Linq;
using CustomLogger;
using PizzaSushiBot.Data;
using static System.Console;
using static PizzaSushiBot.Validator;
using static PizzaSushiBot.SystemStrings;

namespace PizzaSushiBot.Entities
{
    abstract class FormBase
    {
        protected virtual string GetUserEmail()
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

        protected virtual string GetUserName()
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

        protected string InputMaskedPassword()
        {
            string pass = string.Empty;
            ConsoleKey key;

            do
            {
                var keyInfo = Console.ReadKey(true);
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

        private bool EmailTaken(string email)
        {
            using PizzaSushiContext context = new();

            return context.Users.Any(u => u.Email == email);
        }

        private bool UsernameTaken(string username)
        {
            using PizzaSushiContext context = new();

            return context.Users.Any(u => u.Username == username);
        }

        protected long GetUserPhone()
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

        protected string GetUserAddress()
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
    }
}
