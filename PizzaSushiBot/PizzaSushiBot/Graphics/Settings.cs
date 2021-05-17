using System;
using static PizzaSushiBot.SystemStrings;

namespace PizzaSushiBot.Graphics
{
    static class Settings
    {
        public const int MenuWidth = 60;
        public static readonly int HeaderHeight = 10;

        public static void SetConsoleGraphics()
        {
            Console.Title = consoleTitle;
            Console.CursorVisible = false;
        }

        public static void ClearMenuFromLine(int cursorPositionY)
        {
            Console.SetCursorPosition(0, cursorPositionY);
            for (int i = 0; i < 20; i++)
                Console.WriteLine(new string(' ', MenuWidth));

            Console.SetCursorPosition(0, cursorPositionY);
        }

        public static void ResetCursor()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
        }
    }
}
