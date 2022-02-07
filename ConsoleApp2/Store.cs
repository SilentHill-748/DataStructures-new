using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ConsoleApp2
{
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

            var groupedOrders = Orders.GroupBy(order => order.OrderDate.Year);

            foreach (var group in groupedOrders)
            {
                int year = group.Key;

                var maxSelledProduct = group
                    .SelectMany(x => x.Items)
                    .Join(Products, i => i.ProductId, p => p.Id, (i, p) => new
                    {
                        i.ProductId,
                        ProductName = p.Name,
                        p.Price,
                        i.Quantity
                    })
                    .GroupBy(item => item.ProductName)
                    .Select(x => new
                    {
                        ProductName = x.Key,
                        x.First(prod => prod.ProductName == x.Key).Price,
                        x.First(prod => prod.ProductName == x.Key).Quantity,
                        Sells = x.Sum(d => d.Quantity)
                    })
                    .OrderBy(x => x.Quantity)
                    .Last();

                double total = maxSelledProduct.Sells * maxSelledProduct.Price;

                stringBuilder.Append($"{year} - {total} руб.{Environment.NewLine}" +
                    $"Most selling: {maxSelledProduct.ProductName} ({maxSelledProduct.Sells} item(s))\n\r\n\r");
            }

            return stringBuilder.ToString();









            //var statistics = from order in Orders
            //                 from item in order.Items
            //                 join x in Products on item.ProductId equals x.Id
            //                 select new
            //                 {
            //                     order.OrderDate.Year,
            //                     ProductName = x.Name,
            //                     Sells = order.Items.Where(item => item.ProductId == x.Id).Sum(x => x.Quantity),
            //                     x.Price,
            //                 };

            //var res = statistics
            //    .GroupBy(x => x.Year)
            //    .Select(x => x.GroupBy(x => x.ProductName)
            //        .Select(x => new
            //        {
            //            Name = x.Key,
            //            x.First(z => z.ProductName == x.Key).Year,
            //            x.First(z => z.ProductName == x.Key).Price,
            //            Count = x.Sum(x => x.Sells)
            //        })
            //);

            //foreach (var group in res)
            //{
            //    var maxElement = group.OrderBy(x => x.Count).Last();

            //    stringBuilder.Append($"{maxElement.Year} - {maxElement.Count * maxElement.Price} руб.{Environment.NewLine}" +
            //        $"Most selling: {maxElement.Name} ({maxElement.Count} item(s))\r\n\r\n");
            //}

            

            //Console.WriteLine($"Ellapsed: {stopwatch.Elapsed}");
            //return stringBuilder.ToString();
        }

        private void GetAllOrderItems(Order order)
        {

        }
    }
}
