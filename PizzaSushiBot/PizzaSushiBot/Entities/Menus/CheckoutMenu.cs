using System;
using CustomLogger;
using PizzaSushiBot.Models;
using static System.Console;
using System.Collections.Generic;
using static PizzaSushiBot.SystemStrings;
using static PizzaSushiBot.Graphics.Settings;

namespace PizzaSushiBot.Entities.Menus
{
    sealed class CheckoutMenu : Menu<string>
    {
        private readonly string _option1 = "Confirm order".AlignCenter(MenuWidth);

        public CheckoutMenu()
            : base()
        {
            Logger.Info("Opened CheckoutMenu");
            Console.Clear();
            Header.Draw();
            _options.Add(_option1);
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
            CheckIfCartIsEmpty();
        }

        protected override void DrawMenu()
        {
            PrintShoppingList();
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
            WriteLine(new string('-', MenuWidth));
            ResetCursor();
        }

        protected override void NavigateMenu(ConsoleKey key)
        {
            if (key == ConsoleKey.LeftArrow)
                _currentIndex--;
            if (key == ConsoleKey.RightArrow)
                _currentIndex++;
            if (key == ConsoleKey.Escape)
                GoBack();
            if (key == ConsoleKey.Backspace)
                RemoveLastItem(User.GetInstance());

            if (_currentIndex > _options.Count - 1)
                _currentIndex = 0;
            if (_currentIndex < 0)
                _currentIndex = _options.Count - 1;
        }

        private static void PrintShoppingList()
        {
            Dictionary<Product, int> purchases = User.GetInstance().ShoppingCart.Purchases;

            WriteLine($"{"Name", -20}{"Quantity", 10}{"Price", 15}{"Total", 15}");
            WriteLine(new string('-', MenuWidth));

            if(purchases.Count == 0)
                shoppingCartEmptyMessage.AlignCenterAndPrint(MenuWidth);
            else
            {
                foreach (var p in purchases)
                    WriteLine($"{p.Key.Name,-20}{p.Value,10}{p.Key.Price,15}{p.Key.Price * p.Value,15}");

                string totalString = User.GetInstance().ShoppingCart.GrandTotal.ToString();
                string[] grandTotal = new string[] { "GRAND TOTAL", totalString };

                WriteLine(new string('_', MenuWidth));
                WriteLine(grandTotal.AlignedLeftRight('.', MenuWidth));
            }
        }

        public void GoBack()
        {
            new MainMenu().Display();
        }

        private void RemoveLastItem(User user)
        {
            MoveCursorToBottom();

            bool poppable = user.ShoppingCart.ProductStack.TryPeek(out Product p);
            string update = string.Empty;

            if (poppable)
            {
                update = $"Successfully removed: {p.Name}";
                user.ShoppingCart.PopFromCart();
                Logger.Info($"{user} removed {p.Name} from shopping cart.");
            }
                
            update.AlignCenterAndPrint(MenuWidth);
            ReadKey(true);
        }

        private void CheckIfCartIsEmpty()
        {
            int numberOfPurchases = User.GetInstance().ShoppingCart.Purchases.Count;

            if (numberOfPurchases == 0)
            {
                Logger.Info("User force returned to MainMenu (empty cart)");
                MoveCursorToBottom();
                returnToMenuMessage.AlignCenterAndPrint(MenuWidth);
                ReadKey(true);
                new MainMenu().Display();
            }
        }

        private void MoveCursorToBottom()
        {
            int userCarItemsCount = User.GetInstance().ShoppingCart.Purchases.Count;
            int cursorPosY = HeaderHeight + userCarItemsCount + 7;
            SetCursorPosition(0, cursorPosY);
        }
    }
}
