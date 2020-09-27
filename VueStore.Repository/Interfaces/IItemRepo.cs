using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VueStore.Repository.Models;

namespace VueStore.Repository.Interfaces
{
    public interface IItemRepo
    {

        Task<IEnumerable<Item>> GetAll();
        Task<Item> GetItemByID(int id);
        Task<int> CreateItem(Item item);
        Task<int> UpdateItem(Item item);
        Task<int> DeleteItem(int id);        
        Task<IEnumerable<Item>> GetMaxPriceItems();
        Task<Item> GetMaxPriceItem(string name);
    }
}
