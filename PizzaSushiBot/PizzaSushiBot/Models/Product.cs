using System.ComponentModel.DataAnnotations;

namespace PizzaSushiBot.Models
{
    class Product
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
