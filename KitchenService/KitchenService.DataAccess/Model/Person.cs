using System.Collections.Generic;

namespace KitchenService.DataAccess.Model
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<FridgeItem> FridgeItems { get; set; }
    }
}
