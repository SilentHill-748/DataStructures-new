using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp3
{
    public class Program
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
                        UserId = 1,
                        OrderDate = DateTime.UtcNow,
                        Items = new List<Order.OrderItem>
                        {
                            new() { ProductId = 1, Quantity = 1 },
                            new() { ProductId = 2, Quantity = 1 },
                            new() { ProductId = 3, Quantity = 1 }
                        }
                    }
                }
            };

            Console.WriteLine(store.GetProductStatistics(2021));
            Console.WriteLine(store.GetYearsStatistics());
        }
    }

    public class Store
    {
        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }

        /// <summary>
        /// Формирует строку со статистикой продаж продуктов
        /// Сортировка - по убыванию кол-ва проданных продуктов
        /// </summary>
        /// <param name="year">Год, за который подсчитывается статистика</param>
        public string GetProductStatistics(int year)
        {
            // Формат строки:
            // {№}) - {Название продукта} - {кол-во проданных единиц} item(s)\r\n
            //
            // Пример результирующей строки:
            //
            // 1) Product 3 - 1103 item(s)
            // 2) Product 1 - 800 item(s)
            // 3) Product 2 - 10 item(s)

            // TODO Реализовать логику получения и формирования требуемых данных       
            StringBuilder stringBuilder = new();
            int number = 1;

            var statistics = Orders
                .Where(x => x.OrderDate.Year == year)
                .SelectMany(x => x.Items)
                .Join(Products, x => x.ProductId, y => y.Id, (x, y) => new
                {
                    ProductName = y.Name,
                    SellCount = x.Quantity
                })
                .OrderByDescending(x => x.ProductName)
                .GroupBy(x => x.ProductName)
                .Select(x => new
                {
                    Name = x.Key,
                    Sells = x.Sum(x => x.SellCount)
                });

            foreach (var statistic in statistics)
            {
                stringBuilder.Append($"{number}) {statistic.Name} - {statistic.Sells} items(s){Environment.NewLine}");
                number++;
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Формирует строку со статистикой продаж продуктов по годам
        /// Сортировка - по убыванию годов.
        /// Выводятся все года, в которых были продажи продуктов
        /// </summary>
        public string GetYearsStatistics()
        {
            // Формат результата:
            // {Год} - {На какую сумму продано продуктов руб\r\n
            // Most selling: -{Название самого продаваемого продукта} (кол-во проданных единиц самого популярного продукта шт.)\r\n
            // \r\n
            //
            // Пример:
            //
            // 2021 - 630.000 руб.
            // Most selling: Product 1 (380 item(s))
            //
            // 2020 - 630.000 руб.
            // Most selling: Product 1 (380 item(s))
            //
            // 2019 - 130.000 руб.
            // Most selling: Product 3 (10 item(s))
            //
            // 2018 - 50.000 руб.
            // Most selling: Product 3 (5 item(s))

            // TODO Реализовать логику получения и формирования требуемых данных        
            StringBuilder stringBuilder = new();

            var statistics = from order in Orders
                             from item in order.Items
                             join x in Products on item.ProductId equals x.Id
                             orderby order.OrderDate.Year descending
                             select new
                             {
                                 Year = order.OrderDate.Year,
                                 ProductName = x.Name,
                                 MaxSells = order.Items.Where(item => item.ProductId == x.Id).Sum(x => x.Quantity),
                                 Price = Products.Sum(x => x.Price),
                             };

            foreach (var statistic in statistics)
            {
                stringBuilder.Append($"{statistic.Year} - {statistic.Price} руб.{Environment.NewLine}" +
                    $"Most selling: {statistic.ProductName} ({statistic.MaxSells} item(s))");

            }

            return stringBuilder.ToString();
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }

    public class Order
    {
        public int UserId { get; set; }
        public List<OrderItem> Items { get; set; }
        public DateTime OrderDate { get; set; }

        public class OrderItem
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
