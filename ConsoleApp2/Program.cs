using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var store = new Store
            {
                Products = new List<Product>
                {
                    new() { Id = 1, Name = "Product 1", Price = 1000d },
                    new() { Id = 2, Name = "Product 2", Price = 3000d },
                    new() { Id = 3, Name = "Product 3", Price = 10000d }
                },
                Orders = new List<Order>
                {
                    new()
                    {
                        UserId = 1,
                        OrderDate = DateTime.UtcNow,
                        Items = new List<Order.OrderItem>
                        {
                            new() { ProductId = 1, Quantity = 2 }
                        }
                    },
                    new()
                    {
                        UserId = 2,
                        OrderDate = new DateTime(2021, 08, 25),
                        Items = new List<Order.OrderItem>
                        {
                            new() { ProductId = 1, Quantity = 100 },
                            new() { ProductId = 2, Quantity = 1500 },
                            new() { ProductId = 3, Quantity = 1101 }
                        }
                    },
                    new()
                    {
                        UserId = 3,
                        OrderDate = new DateTime(2021, 11, 16),
                        Items = new List<Order.OrderItem>
                        {
                            new() { ProductId = 1, Quantity = 100 },
                            new() { ProductId = 2, Quantity = 1500 },
                            new() { ProductId = 3, Quantity = 1101 }
                        }
                    }

                }
            };

            Console.WriteLine(store.GetProductStatistics(2021));
            Console.WriteLine(store.GetYearsStatistics());
        }
    }

    class Test
    {
        public int Year { get; set; }

        public int Price { get; set; }
    }
}
