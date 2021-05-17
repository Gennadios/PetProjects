using System;
using PizzaSushiBot.Models;
using static System.Console;
using static PizzaSushiBot.SystemStrings;
using static PizzaSushiBot.Graphics.Settings;

namespace PizzaSushiBot
{
    static class Header
    {
        static readonly string title =
            @"
    ___ _              __           _     _   ___       _   
   / _ (_)__________ _/ _\_   _ ___| |__ (_) / __\ ___ | |_ 
  / /_)/ |_  /_  / _` \ \| | | / __| '_ \| |/__\/// _ \| __|
 / ___/| |/ / / / (_| |\ \ |_| \__ \ | | | / \/  \ (_) | |_ 
 \/    |_/___/___\__,_\__/\__,_|___/_| |_|_\_____/\___/ \__|";

        internal static void Draw()
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine(slogan);
            WriteLine(title);
            ForegroundColor = ConsoleColor.DarkMagenta;
            WriteLine(userInstructions);
            WriteLine(userInstructions2);
            ResetColor();
            GreetUser();
        }

        static void GreetUser()
        {
            string greeting;

            if (string.IsNullOrEmpty(User.GetInstance().Username))
                greeting = standardUserGreeting;
            else
                greeting = $"Welcome, {User.GetInstance().Username}!";

            WriteLine($"{greeting, MenuWidth}");
        }
    }
}
