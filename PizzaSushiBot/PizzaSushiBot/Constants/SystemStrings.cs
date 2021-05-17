namespace PizzaSushiBot
{
    public static class SystemStrings
    {
        #region HEADER
        public static readonly string consoleTitle = "PIZZASUSHIBOT version 1.0";
        public static readonly string slogan = "********************* WE'RE ON A ROLL **********************";
        public static readonly string userInstructions = " Arrow Keys = navigation, <ENTER> = select, <ESC> = go back";
        public static readonly string userInstructions2 = " Food menu: <ENTER> to add item, <BACKSPACE> to remove item";
        public static readonly string standardUserGreeting = "Welcome, guest!";
        #endregion
        #region MENU MESSAGES
        public static readonly string welcomeMenuMessage = "Welcome! Would you like to login or continue as guest?";
        public static readonly string mainMenuMessage = "What would you like today?";
        public static readonly string pizzaMenuMessage = "All the best things are round!";
        public static readonly string sushiMenuMessage = "Fish and rice are soy nice!";
        public static readonly string wokMenuMessage = "Let's take a wok... to your place!";
        #endregion
        #region REGISTRATION
        public static readonly string usernameTakenMessage = "Username is already taken! Please, try another.";
        public static readonly string usernameInvalidMessage = "Please, input a valid username [4-16 characters].";
        public static readonly string emailTakenMessage = "Sorry, this email is already taken. Please, try another.";
        public static readonly string emailInvalidMessage = "Please input a valid email!";
        public static readonly string phoneInvalidMessage = "Please input a valid phone number[10 digits]!";
        public static readonly string addressInvalidMessage = "Please input a valid address [City, Street, Block #, apartment #]!";
        public static readonly string passwordInvalidMessage = "Password should at least be 6 characters long.";
        public static readonly string pressKeyPrompt = "Press any key to continue.";
        #endregion
        #region LOGIN
        public static readonly string loginFailedMessage = "Password or username incorrect.";
        public static readonly string guestOrRegisterMessage = "Please, continue as guest, register or restart app."; 
        #endregion
        #region SHOPPING MESSAGES
        public static readonly string shoppingCartEmptyMessage = "Your shopping cart is empty!";
        public static readonly string returnToMenuMessage = "Your order is empty! Please, go back to main menu.";
        public static readonly string deliveryAddressPrompt = "Please, enter your delivery address.";
        public static readonly string orderConfirmedMessage = "Great! Your order has been confirmed!";
        public static readonly string orderIsReadyMessage = "Your order is ready! Have a good meal :)";
        #endregion
    }
}
