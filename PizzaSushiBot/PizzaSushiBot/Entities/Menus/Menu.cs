using System;
using static System.Console;
using System.Collections.Generic;

namespace PizzaSushiBot.Entities.Menus
{
    abstract class Menu<T> where T : class
    {
        protected int _currentIndex;
        protected List<T> _options = new();
        protected string _message;

        public Menu()
        {
            _currentIndex = 0;
        }

        public abstract void Display();

        protected abstract void DrawMenu();

        protected abstract void NavigateMenu(ConsoleKey key);

        protected virtual void HighlightOption(string option)
        {
            ForegroundColor = ConsoleColor.Black;
            BackgroundColor = ConsoleColor.White;
            Write(option);
            ResetColor();
        }
    }
}
