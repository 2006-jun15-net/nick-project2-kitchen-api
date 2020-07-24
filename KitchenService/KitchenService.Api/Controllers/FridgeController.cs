using System;
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
        public async Task<IActionResult> GetItemsAsync()
        {
            IEnumerable<Core.FridgeItem> items = await _fridgeItemRepository.GetAsync();
            var resource = items.Select(Map);

            return Ok(resource);
        }

        // GET api/fridge/items/5
        [HttpGet("items/{id}")]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            if (await _fridgeItemRepository.GetAsync(id) is FridgeItem item)
            {
                return Ok(item);
            }
            return NotFound();
        }

        // POST api/fridge/items
        [HttpPost("items")]
        [ProducesResponseType(typeof(FoodItem), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostItemAsync([FromBody, Bind("Name,ExpirationDate")] FoodItem resource)
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
