using System;
using PizzaSushiBot.Entities.Menus;

namespace PizzaSushiBot.Interfaces
{
    interface INavigatable
    {
        void GoToOption(int optionNumber);
    }
}
