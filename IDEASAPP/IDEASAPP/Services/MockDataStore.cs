using IDEASAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDEASAPP.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Diego", Description="Es un lugar genial, cerca de tiendas y restaurantes, muy tranquilo." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "María", Description="Para ser honesta, esta es la mejor panadria en la que he comprado. El servicio es súper bueno, el sabor del pan no se puede describir. Te lo recomiendo." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Daniela", Description="La ubicación de la panadería es fácil de encontrar y hay servidio de estacionamiento gratuito. El cajero fue muy educado, muy satisfecha con el servicio." }

            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}