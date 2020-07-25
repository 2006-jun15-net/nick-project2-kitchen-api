using System;

namespace KitchenService.Api.ApiModels
{
    public class FoodItemWithoutId
    {
        public string Name { get; set; }

        public DateTimeOffset? ExpirationDate { get; set; }
    }
}
