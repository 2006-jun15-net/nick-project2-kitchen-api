using System.Collections.Generic;
using System.Threading.Tasks;

namespace KitchenService.Core.Interfaces
{
    public interface IFridgeItemRepository
    {
        Task<IEnumerable<FridgeItem>> GetAsync();

        Task<FridgeItem> GetAsync(int id);

        Task<int> CreateAsync(FridgeItem item);

        Task<bool> DeleteAsync(int id);

        Task DeleteAsync(IEnumerable<int> ids);
    }
}
