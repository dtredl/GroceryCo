using GroceryCo.Models;
using System.Collections.Generic;

namespace GroceryCo.Interfaces
{
    /// <summary>
    /// This interface defines what is needed from an inventory repository, whether that repository access an xml file, database, service, or anything else.
    /// </summary>
    interface IInventoryRepository
    {
        IEnumerable<InventoryItem> GetInventoryItems();
    }
}
