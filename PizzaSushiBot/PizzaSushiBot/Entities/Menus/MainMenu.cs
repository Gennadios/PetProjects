using System;
using CustomLogger;
using static System.Console;
using PizzaSushiBot.Interfaces;
using static PizzaSushiBot.SystemStrings;
using static PizzaSushiBot.Graphics.Settings;

namespace PizzaSushiBot.Entities.Menus
{
    sealed class MainMenu : Menu<string>, INavigatable
    {
        private readonly string _option1 = "Pizza".AlignCenter(15);
        private readonly string _option2 = "Sushi".AlignCenter(15);
        private readonly string _option3 = "Wok".AlignCenter(15);
        private readonly string _option4 = "Cart ".AlignCenter(15);

        public MainMenu()
            : base()
        {
            Logger.Info("MainMenu opened.");
            Clear();
            Header.Draw();

            _message = mainMenuMessage;

            _options.Add(_option1);
            _options.Add(_option2);
            _options.Add(_option3);
            _options.Add(_option4);
        }

        public override void Display()
        {
            ConsoleKey pressedKey;
            do
            {
                ClearMenuFromLine(10);
                DrawMenu();
                
                pressedKey = Console.ReadKey(true).Key;
                NavigateMenu(pressedKey);
            } while (pressedKey != ConsoleKey.Enter);
            GoToOption(_currentIndex);
        }

        protected override void DrawMenu()
        {
            _message.AlignCenterAndPrint(MenuWidth);
            WriteLine(new string('-', MenuWidth));
            for (int i = 0; i < _options.Count; i++)
            {
                string currentOption = _options[i];

                if (i == _currentIndex)
                    HighlightOption(currentOption);
                else
                    Write($"{currentOption}");
            }
            WriteLine();
            Footer.DisplayShoppingInfo();
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
                    new FoodMenu("pizza", pizzaMenuMessage).Display();
                    break;
                case 1:
                    new FoodMenu("sushi", sushiMenuMessage).Display();
                    break;
                case 2:
                    new FoodMenu("wok", wokMenuMessage).Display();
                    break;
                case 3:
                    new CheckoutMenu().Display();
                    break;
            }
        }
    }
}
