using System;
using System.Collections.Generic;
using System.Linq;
using KitchenService.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KitchenService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeController : ControllerBase
    {
        private static readonly List<FoodItem> s_contents = new List<FoodItem>
        {
            new FoodItem { Id = 1, Name = "cheese", ExpirationDate = new DateTime(2020, 6, 14) },
            new FoodItem { Id = 2, Name = "steak", ExpirationDate = new DateTime(2020, 7, 28) },
            new FoodItem { Id = 3, Name = "salmon", ExpirationDate = new DateTime(2020, 7, 28) }
        };

        // GET: api/fridge/items
        [HttpGet("items")]
        public IActionResult GetItems()
        {
            return Ok(s_contents);
        }

        // GET api/fridge/items/5
        [HttpGet("items/{id}")]
        public ActionResult<FoodItem> GetItem(int id)
        {
            if (s_contents.FirstOrDefault(x => x.Id == id) is FoodItem item)
            {
                return item;
            }
            //return StatusCode(418, new object());
            return NotFound();
            //return Content("<!doctype html><p>data</p>", "application/html", Encoding.UTF8);
        }

        // POST api/fridge/items
        [HttpPost("items")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(FoodItem), StatusCodes.Status201Created)]
        public IActionResult PostItem([FromBody] FoodItem item)
        {
            if (s_contents.Any(x => x.Id == item.Id))
            {
                return Conflict();
            }
            s_contents.Add(item);
            return CreatedAtAction(
                actionName: nameof(GetItem),
                routeValues: new { id = item.Id },
                value: item);
        }

        // PUT api/fridge/items/5
        [HttpPut("items/{id}")]
        public void PutItem(int id, [FromBody] string value)
        {
        }

        // DELETE api/fridge/items/5
        [HttpDelete("items/{id}")]
        public void DeleteItem(int id)
        {
        }
    }
}
