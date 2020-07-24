using System;

namespace KitchenService.Api.ApiModels
{
    public class FoodItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset? ExpirationDate { get; set; }
    }
}
