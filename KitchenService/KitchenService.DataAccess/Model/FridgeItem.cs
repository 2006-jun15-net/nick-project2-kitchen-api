using System;

namespace KitchenService.DataAccess.Model
{
    public class FridgeItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset? ExpirationDate { get; set; }

        public Person Owner { get; set; }
    }
}
