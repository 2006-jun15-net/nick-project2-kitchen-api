using System;

namespace KitchenService.Core
{
    public class FridgeItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset? ExpirationDate { get; set; }
    }
}
