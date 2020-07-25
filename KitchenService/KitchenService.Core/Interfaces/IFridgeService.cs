using System.Collections.Generic;
using System.Threading.Tasks;

namespace KitchenService.Core.Interfaces
{
    public interface IFridgeService
    {
        Task<IEnumerable<FridgeItem>> CleanAsync();
    }
}
