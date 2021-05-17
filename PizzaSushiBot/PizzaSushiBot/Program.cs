using PizzaSushiBot.Entities;
using PizzaSushiBot.Services;
using PizzaSushiBot.Entities.Menus;
using static PizzaSushiBot.Graphics.Settings;

namespace PizzaSushiBot
{
    class Program
    {
        static void Main(string[] args)
        {
            MailService.GetInstance();

            SetConsoleGraphics();
            new WelcomeMenu().Display();
            new MainMenu().Display();
            OrderConfirmation.GetInstance().Initialize();
        }
    }
}
