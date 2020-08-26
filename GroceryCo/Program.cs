using System;
using System.Collections.Generic;
using System.IO;

using GroceryCo.Repositories;

namespace GroceryCo
{
    /// <summary>
    /// This is the bootstrapper for the program.
    /// It reads the order from a file to simulate the order being scanned/entered at a grocery store kiosk.
    /// It then creates an instance of an IInventoryRepository and injects that as a dependency to the OrderCalculator to process the order.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var stream = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "order.txt"));
            var order = new Dictionary<string, int>();
            string scannedItem;
            while ((scannedItem = stream.ReadLine()) != null)
            {
                scannedItem = scannedItem.ToLower();

                if (string.IsNullOrWhiteSpace(scannedItem))
                {
                    continue;
                }

                if (order.ContainsKey(scannedItem))
                {
                    order[scannedItem]++;
                }
                else
                {
                    order.Add(scannedItem, 1);
                }
            }

            var inventoryRepository = new XmlInventoryRepository("store.xml");
            var orderCalculator = new OrderCalculator(inventoryRepository);

            Console.Write(orderCalculator.CalculateOrderOutput(order));
            Console.ReadKey();
        }
    }
}
