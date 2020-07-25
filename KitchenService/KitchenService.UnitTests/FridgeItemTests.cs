using KitchenService.Core;
using Xunit;

namespace KitchenService.UnitTests
{
    public class FridgeItemTests
    {
        [Fact]
        public void ConstructorConstructsWithDefaults()
        {
            var item = new FridgeItem();

            Assert.Equal(expected: 0, actual: item.Id);
            Assert.Null(item.Name);
            Assert.Null(item.ExpirationDate);
        }
    }
}
