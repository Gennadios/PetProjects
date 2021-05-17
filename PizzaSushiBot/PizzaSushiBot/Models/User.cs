using PizzaSushiBot.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaSushiBot.Models
{
    class User
    {
        private static User _instance;
        private readonly Cart _cart = new();

        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public long Phone { get; set; }
        public string Address { get; set; }
        [NotMapped]
        public Cart ShoppingCart { get => _cart; }

        private User() { }

        public static User GetInstance()
        {
            if (_instance == null)
                _instance = new User();

            return _instance;
        }

        public override string ToString()
        {
            return $"Username: {Username}, ID: {Id}";
        }
    }
}
