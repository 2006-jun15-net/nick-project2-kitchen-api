using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenService.Core.Interfaces;
using KitchenService.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace KitchenService.DataAccess.Repositories
{
    public class FridgeItemRepository : IFridgeItemRepository
    {
        private readonly KitchenContext _context;

        public FridgeItemRepository(KitchenContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Core.FridgeItem>> GetAsync()
        {
            List<FridgeItem> items = await _context.FridgeItems
                .ToListAsync();

            return items.Select(Map);
        }

        public async Task<Core.FridgeItem> GetAsync(int id)
        {
            var item = await _context.FridgeItems.FindAsync(id);

            if (item is null)
            {
                return null;
            }
            return Map(item);
        }

        public async Task<int> CreateAsync(Core.FridgeItem item)
        {
            var entity = new FridgeItem
            {
                Name = item.Name,
                ExpirationDate = item.ExpirationDate
            };

            await _context.FridgeItems.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            FridgeItem item = await _context.FridgeItems.FindAsync(id);
            if (item is null)
            {
                return false;
            }
            _context.FridgeItems.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAsync(IEnumerable<int> ids)
        {
            var idList = ids.ToList();
            List<FridgeItem> items = await _context.FridgeItems
                .Where(i => ids.Contains(i.Id))
                .ToListAsync();
            if (items.Count != idList.Count)
            {
                throw new ArgumentException("Some item IDs did not exist", nameof(ids));
            }
            _context.FridgeItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        private static Core.FridgeItem Map(FridgeItem item)
        {
            return new Core.FridgeItem
            {
                Id = item.Id,
                Name = item.Name,
                ExpirationDate = item.ExpirationDate
            };
        }
    }
}
