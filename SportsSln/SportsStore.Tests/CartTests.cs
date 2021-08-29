using System.Linq;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            // arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Cart target = new Cart();

            // act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            // assert
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Cart target = new Cart();


            // act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);

            CartLine[] results = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();

            // assert
            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            // arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };
            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p3, 1);

            // act
            target.RemoveLine(p2);

            CartLine[] results = target.Lines.OrderBy(l => l.Product.ProductID).ToArray();

            // assert
            Assert.Equal(2, results.Length);
            Assert.Empty(target.Lines.Where(c => c.Product == p2));
        }

        [Fact]
        public void Can_Calculate_Cart_Total()
        {
            // arrange
            Product p1 = new Product { ProductID = 1, Price = 10M };
            Product p2 = new Product { ProductID = 2, Price = 15M  };
            Product p3 = new Product { ProductID = 3, Price = 20.5M };

            Cart target = new Cart();

            // act
            target.AddItem(p1, 1);
            target.AddItem(p2, 2);
            target.AddItem(p3, 1);

            decimal actual = target.ComputeTotalValue();

            // assert
            Assert.Equal(60.5M, actual);
        }

        [Fact]
        public void Can_Clear_Cart_Contents()
        {
            // arrange
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 10M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 15M };

            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 2);

            // act
            target.Clear();

            // assert
            Assert.Empty(target.Lines);
        }
    }
}
