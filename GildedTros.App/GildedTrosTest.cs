using System.Collections.Generic;
using Xunit;

namespace GildedTros.App
{
    public class GildedTrosTest
    {
        [Fact]
        public void ItemsAddedToList()
        {
            IList<Item> Items = new List<Item> { 
                new Item { Name = "item1", SellIn = 5, Quality = 10 },
                new Item { Name = "item2", SellIn = 10, Quality = 15 }};
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();

            Assert.Equal("item1", Items[0].Name);
            Assert.Equal("item2", Items[1].Name);
        }

        [Fact]
        public void ItemUpdateSellInAndQuality()
        {
            IList<Item> Items = new List<Item> { 
                new Item { Name = "item1", SellIn = 5, Quality = 10 }
                };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            
            Assert.Equal(4, Items[0].SellIn);
            Assert.Equal(9, Items[0].Quality);
        }

        [Fact]
        public void ItemDegradeSellByDatePassed()
        {
            IList<Item> Items = new List<Item> {
                new Item { Name = "item1", SellIn = 0, Quality = 10 },
                };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();

            Assert.Equal(-1, Items[0].SellIn);
            Assert.Equal(8, Items[0].Quality);
        }

        [Fact]
        public void ItemQualityNeverNegative()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "item1", SellIn = 5, Quality = 0 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            
            Assert.Equal(4, Items[0].SellIn);
            Assert.Equal(0, Items[0].Quality);
        }

        [Fact]
        public void ItemGoodWineIncreasesQuality()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Good Wine", SellIn = 5, Quality = 10 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();

            Assert.Equal(4, Items[0].SellIn);
            Assert.Equal(11, Items[0].Quality);
        }

        [Fact]
        public void ItemGoodWineIncreases2QualityAfterSellInPassed()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Good Wine", SellIn = -1, Quality = 10 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();

            Assert.Equal(-2, Items[0].SellIn);
            Assert.Equal(12, Items[0].Quality);
        }

        [Fact]
        public void ItemQualityNeverAbove50()
        {
            IList<Item> Items = new List<Item> {
                new Item { Name = "Good Wine", SellIn = 5, Quality = 50 },
                new Item { Name = "Backstage passes for Re:factor", SellIn = 5, Quality = 49 }
                };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();

            Assert.Equal(4, Items[0].SellIn); 
            Assert.Equal(50, Items[0].Quality);

            Assert.Equal(4, Items[1].SellIn);
            Assert.Equal(50, Items[1].Quality);
        }

        [Fact]
        public void ItemBDAWGKeychainNeverSellsOrChangesQuality()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "B-DAWG Keychain", SellIn = 5, Quality = 10 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();

            Assert.Equal(5, Items[0].SellIn);
            Assert.Equal(10, Items[0].Quality);
        }

        [Fact]
        public void ItemBackstagePassesIncreaseInQuality()
        {//Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less
            IList<Item> Items = new List<Item> { 
                new Item { Name = "Backstage passes for Re:factor", SellIn = 15, Quality = 10 }
            };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();

            Assert.Equal(14, Items[0].SellIn);
            Assert.Equal(11, Items[0].Quality);
        }

        [Fact]
        public void ItemBackstagePassesIncrease2InQuality10DaysOrLess()
        {//Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less
            IList<Item> Items = new List<Item> {
                new Item { Name = "Backstage passes for Re:factor", SellIn = 10, Quality = 10 }
            };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();

            Assert.Equal(9, Items[0].SellIn);
            Assert.Equal(12, Items[0].Quality);
        }

        [Fact]
        public void ItemBackstagePassesIncrease3InQuality5DaysOrLess()
        {//Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less
            IList<Item> Items = new List<Item> {
                new Item { Name = "Backstage passes for Re:factor", SellIn = 5, Quality = 10 }
            };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();

            Assert.Equal(4, Items[0].SellIn);
            Assert.Equal(13, Items[0].Quality);
        }

        [Fact]
        public void ItemBackstagePassesDropQualityTo0AfterConference()
        {//Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but
            IList<Item> Items = new List<Item> {
                new Item { Name = "Backstage passes for Re:factor", SellIn = 0, Quality = 10 },
                new Item { Name = "Backstage passes for HAXX", SellIn = -1, Quality = 10 }

            };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();

            Assert.Equal(-1, Items[0].SellIn);
            Assert.Equal(0, Items[0].Quality);

            Assert.Equal(-2, Items[1].SellIn);
            Assert.Equal(0, Items[1].Quality);
        }
    }
}