
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public class InMemItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { Id= Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id= Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id= Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow },
        };

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item GetItem(Guid id)
        {
         
            var item = items.Where(item => item.Id == id).SingleOrDefault();
            return item;
            // if (item is null){
            //     throw new NullReferenceException($"Unable to find item with id {id}");
            // }
            // return item;
            // return items.Where(item => item.Id == id).SingleOrDefault(); // Finds: return item, Not FindL Returns No.
        }
    }
}