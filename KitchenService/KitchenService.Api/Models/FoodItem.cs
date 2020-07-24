using System;

namespace KitchenService.Api.Models
{
    public class FoodItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
