using System;
using CustomLogger;
using PizzaSushiBot.Models;
using static System.Console;
using PizzaSushiBot.Interfaces;
using static PizzaSushiBot.Graphics.Settings;

namespace PizzaSushiBot.Entities.Menus
{
    sealed class WelcomeMenu : Menu<string>, INavigatable
    {
        private readonly string _option1 = "Login ".AlignCenter(20);
        private readonly string _option2 = "Continue as guest ".AlignCenter(20);
        private readonly string _option3 = "Register".AlignCenter(20);

        public WelcomeMenu()
            : base()
        {
            Logger.Info("WelcomeMenu opened.");
            Clear();
            Header.Draw();

            _options.Add(_option1);
            _options.Add(_option2);
            _options.Add(_option3);
        }

        public override void Display()
        {
            ConsoleKey pressedKey;
            do
            {
                ClearMenuFromLine(HeaderHeight);
                DrawMenu();

                pressedKey = Console.ReadKey(true).Key;
                NavigateMenu(pressedKey);
            } while (pressedKey != ConsoleKey.Enter);
            GoToOption(_currentIndex);
        }

        protected override void DrawMenu()
        {
            Console.WriteLine(new string('-', MenuWidth));
            for (int i = 0; i < _options.Count; i++)
            {
                string currentOption = _options[i];

                if (i == _currentIndex)
                    HighlightOption(currentOption);
                else
                    Console.Write($"{currentOption}");
            }
            WriteLine();
            WriteLine(new string('-', MenuWidth));
            ResetCursor();
        }

        protected override void NavigateMenu(ConsoleKey key)
        {
            if (key == ConsoleKey.LeftArrow)
                _currentIndex--;
            if (key == ConsoleKey.RightArrow)
                _currentIndex++;

            if (_currentIndex > _options.Count - 1)
                _currentIndex = 0;
            if (_currentIndex < 0)
                _currentIndex = _options.Count - 1;
        }

        public void GoToOption(int option)
        {
            switch (_currentIndex)
            {
                case 0:
                    Login.Initialize();
                    break;
                case 1:
                    User.GetInstance();
                    break;
                case 2:
                    Registration.GetInstance().Initialize();
                    break;
            }
        }
    }
}
