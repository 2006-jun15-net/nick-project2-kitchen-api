using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenService.Api.ApiModels;
using KitchenService.Core;
using KitchenService.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KitchenService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeController : ControllerBase
    {
        private readonly IFridgeItemRepository _fridgeItemRepository;

        public FridgeController(IFridgeItemRepository fridgeItemRepository)
        {
            _fridgeItemRepository = fridgeItemRepository;
        }

        // GET: api/fridge/items
        [HttpGet("items")]
        [ProducesResponseType(typeof(IEnumerable<FoodItem>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetItemsAsync()
        {
            IEnumerable<FridgeItem> items = await _fridgeItemRepository.GetAsync();
            var resource = items.Select(Map);

            return Ok(resource);
        }

        // POST api/fridge/items
        [HttpPost("items")]
        [ProducesResponseType(typeof(FoodItem), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostItemAsync([FromBody] FoodItemWithoutId resource)
        {
            var item = new FridgeItem
            {
                Name = resource.Name,
                ExpirationDate = resource.ExpirationDate
            };

            var id = await _fridgeItemRepository.CreateAsync(item);

            var created = new FoodItem
            {
                Id = id,
                Name = resource.Name,
                ExpirationDate = resource.ExpirationDate
            };

            return CreatedAtAction(
                actionName: nameof(GetItemAsync),
                routeValues: new { id = item.Id },
                value: created);
        }

        // GET api/fridge/items/5
        [HttpGet("items/{id}")]
        [ProducesResponseType(typeof(FoodItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            if (await _fridgeItemRepository.GetAsync(id) is FridgeItem item)
            {
                return Ok(item);
            }
            return NotFound();
        }

        // POST: api/fridge/clean
        [HttpPost("clean")]
        [ProducesResponseType(typeof(IEnumerable<FoodItemWithoutId>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CleanAsync([FromServices] IFridgeService fridgeService)
        {
            IEnumerable<FridgeItem> removed = await fridgeService.CleanAsync();

            var removedResources = removed.Select(i => new FoodItemWithoutId
            {
                Name = i.Name,
                ExpirationDate = i.ExpirationDate
            });

            return Ok(removedResources);
        }

        private static FoodItem Map(FridgeItem item)
        {
            return new FoodItem
            {
                Id = item.Id,
                Name = item.Name,
                ExpirationDate = item.ExpirationDate
            };
        }
    }
}
