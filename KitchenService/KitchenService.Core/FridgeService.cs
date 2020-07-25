using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenService.Core.Interfaces;

namespace KitchenService.Core
{
    public class FridgeService : IFridgeService
    {
        private readonly IFridgeItemRepository _itemRepository;

        public FridgeService(IFridgeItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<FridgeItem>> CleanAsync()
        {
            var allItems = await _itemRepository.GetAsync();
            var expired = allItems
                .Where(i => i.ExpirationDate <= DateTime.Now)
                .ToList();
            await _itemRepository.DeleteAsync(expired.Select(i => i.Id));
            foreach (var item in expired)
            {
                item.Id = 0;
            }
            return expired;
        }
    }
}
