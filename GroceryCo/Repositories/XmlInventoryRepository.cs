using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

using GroceryCo.Models;
using GroceryCo.Interfaces;

namespace GroceryCo.Repositories
{
    /// <summary>
    /// This repository provides access to the store's inventory as stored in an xml file
    /// </summary>
    class XmlInventoryRepository : IInventoryRepository
    {
        static XDocument data;

        public XmlInventoryRepository(string fileName)
        {
            data = XDocument.Load(Path.Combine(Directory.GetCurrentDirectory(), fileName));
        }

        public IEnumerable<InventoryItem> GetInventoryItems()
        {
            return from i in data.Root.Descendants("InventoryItem")
                   select new InventoryItem
                   {
                       Name = (string)i.Attribute("name"),
                       UnitPrice = (double)i.Attribute("unitPrice"),
                       Promotions = GetItemPromotions(i)
                   };
        }

        private IEnumerable<Promotion> GetItemPromotions(XElement item)
        {
            var promotions = new List<Promotion>();
            promotions.AddRange(from p in item.Descendants("BasicPromotion")
                                select new BasicPromotion
                                {
                                    StartDate = DateTime.Parse((string)p.Attribute("startDate")),
                                    EndDate = DateTime.Parse((string)p.Attribute("endDate")),
                                    SaleUnitPrice = (double)p.Attribute("saleUnitPrice")
                                });
            promotions.AddRange(from p in item.Descendants("MinimumQuantityPromotion")
                                select new MinimumQuantityPromotion
                                {
                                    StartDate = DateTime.Parse((string)p.Attribute("startDate")),
                                    EndDate = DateTime.Parse((string)p.Attribute("endDate")),
                                    MinimumQuantity = (int)p.Attribute("minimumQuantity"),
                                    SaleUnitPrice = (double)p.Attribute("saleUnitPrice")
                                });
            promotions.AddRange(from p in item.Descendants("GroupPromotion")
                                select new GroupPromotion
                                {
                                    StartDate = DateTime.Parse((string)p.Attribute("startDate")),
                                    EndDate = DateTime.Parse((string)p.Attribute("endDate")),
                                    GroupPrice = (double)p.Attribute("groupPrice"),
                                    GroupSize = (int)p.Attribute("groupSize")
                                });
            promotions.AddRange(from p in item.Descendants("AdditionalProductPromotion")
                                select new AdditionalProductPromotion
                                {
                                    StartDate = DateTime.Parse((string)p.Attribute("startDate")),
                                    EndDate = DateTime.Parse((string)p.Attribute("endDate")),
                                    AdditionalProductDiscount = (double)p.Attribute("additionalItemDiscount")
                                });
            return promotions;
        }
    }
}
