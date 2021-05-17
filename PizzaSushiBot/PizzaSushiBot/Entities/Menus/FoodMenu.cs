using System;
using System.Linq;
using CustomLogger;
using PizzaSushiBot.Data;
using PizzaSushiBot.Models;
using System.Data.SqlClient;
using static System.Console;
using System.Collections.Generic;
using static PizzaSushiBot.DBManager;
using static PizzaSushiBot.SystemStrings;
using static PizzaSushiBot.Graphics.Settings;

namespace PizzaSushiBot.Entities.Menus
{
    sealed class FoodMenu : Menu<Product>
    {
        private string[] _columns;

        public FoodMenu()
            : base() { }

        public FoodMenu(string type, string message) 
            : base()
        {
            Logger.Info($"Opened {type} menu");
            _message = message;
            _options = InitializeCollection(type);
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
            } while (pressedKey != ConsoleKey.Escape);
            GoBack();
        }

        protected override void DrawMenu()
        {
            _message.AlignCenterAndPrint(MenuWidth);

            _columns = new string[] { "[Name]", "[Price]" };
            WriteLine(_columns.AlignedLeftRight('_', MenuWidth));
            for (int i = 0; i < _options.Count; i++)
            {
                _columns = new string[] {_options[i].Name, _options[i].Price.ToString()};
                string currentOption = _columns.AlignedLeftRight('.', MenuWidth);

                if (i == _currentIndex)
                    HighlightOption(currentOption);
                else
                    WriteLine($"{currentOption}");
            }
            Footer.DisplayShoppingInfo();
            ResetCursor();
        }

        protected override void NavigateMenu(ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow)
                _currentIndex--;
            if (key == ConsoleKey.DownArrow)
                _currentIndex++;
            if (key == ConsoleKey.Enter)
                AddItem(User.GetInstance(), _options[_currentIndex]);
            if (key == ConsoleKey.Backspace)
                RemoveLastItem(User.GetInstance());

            if (_currentIndex > _options.Count - 1)
                _currentIndex = 0;
            if (_currentIndex < 0)
                _currentIndex = _options.Count - 1;
        }

        [Obsolete]
        private static List<Product> InitializeCollectionOld(string tableName)
        {
            List<Product> list = new();
            OpenDataBaseConnection();

            SQLCommand = new SqlCommand($"SELECT * FROM {tableName}", SQLConnection);
            SQLDataReader = SQLCommand.ExecuteReader();

            while (SQLDataReader.Read())
            {
                list.Add(new Product
                {
                    Id = Convert.ToInt32(SQLDataReader["id"]),
                    Name = SQLDataReader["name"].ToString(),
                    Price = Convert.ToDecimal(SQLDataReader["price"])
                });
            }

            CloseDataBaseConnection();
            return list;
        }

        private static List<Product> InitializeCollection(string type)
        {
            using PizzaSushiContext context = new();

            var list = 
                context.Products
                .Where(p => p.Type == type)
                .ToList();

            Logger.Info($"Initialized {type} menu");
            return list;
        }

        public void GoBack()
        {
            Logger.Info($"Went back to MainMenu");
            new MainMenu().Display();
        }

        private void AddItem(User user, Product product)
        {
            int cursorPosY = HeaderHeight + _options.Count + 3;
            SetCursorPosition(0, cursorPosY);
            string update = $"Successfully added: {product.Name}";

            user.ShoppingCart.AddToCart(product);
            update.AlignCenterAndPrint(MenuWidth);

            ReadKey(true);
            Logger.Info($"Added {product.Name} from shopping cart");
        }

        private void RemoveLastItem(User user)
        {
            int cursorPosY = HeaderHeight + _options.Count + 3;
            SetCursorPosition(0, cursorPosY);

            bool poppable = user.ShoppingCart.ProductStack.TryPeek(out Product p);
            string update;

            if (poppable)
            {
                update = $"Successfully removed: {p.Name}";
                Logger.Info($"Removed {p.Name} from shopping cart");
            }
            else
                update = shoppingCartEmptyMessage;

            user.ShoppingCart.PopFromCart();
            update.AlignCenterAndPrint(MenuWidth);

            ReadKey(true);
        }
    }
}
