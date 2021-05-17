using PizzaSushiBot.Models;
using static System.Console;
using static PizzaSushiBot.Graphics.Settings;

namespace PizzaSushiBot.Entities.Menus
{
    sealed class Footer
    {
        internal static void DisplayShoppingInfo()
        {
            Cart userCart = User.GetInstance().ShoppingCart;

            WriteLine(new string('_', MenuWidth));
            $"Items in shopping cart: {userCart.NumberOfItems}".AlignCenterAndPrint(MenuWidth);
            $"Value: {userCart.GrandTotal} RUB".AlignCenterAndPrint(MenuWidth);
        }
    }
}
