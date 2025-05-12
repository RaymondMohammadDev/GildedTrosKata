using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GildedTros.App
{
    public class GildedTros
    {
        IList<Item> Items;
        public GildedTros(IList<Item> Items)
        {
            this.Items = Items;
        }

        private int AddQuality(int quality, int amountToAdd = 1)
        {
            if (quality < 50)
                return quality+amountToAdd;
            return quality;
        }

        private int RemoveQuality(Item item, int amount = 1)
        {
            int newItemQuality = item.Quality;
            int amountToRemove = amount;
            if (item.Quality > 0)
            {
                newItemQuality = item.Quality - amountToRemove;

                if (item.SellIn <= 0 && item.Quality > 1)
                {
                    newItemQuality = newItemQuality - amountToRemove;
                }
            }
            return newItemQuality;
        }

        public void UpdateQuality()
        {
            foreach (Item item in Items)
            {
                switch (item.Name) 
                {
                    case "Good Wine":
                        item.Quality = AddQuality(item.Quality);
                        break;
                    case "B-DAWG Keychain":
                        break;
                    case var n when item.Name.Contains("Backstage passes"):
                        switch (item.SellIn)
                        {
                            case 0:
                                item.Quality = 0;
                                break;
                            case < 6:
                                item.Quality = AddQuality(item.Quality, 3);
                                break;
                            case < 11:
                                item.Quality = AddQuality(item.Quality, 2);
                                break;
                            default:
                                item.Quality = AddQuality(item.Quality);
                                break;
                        }
                        break;
                    default:
                        item.Quality = RemoveQuality(item);
                        break;
                }

                if (item.Name != "B-DAWG Keychain")
                {
                    item.SellIn = item.SellIn - 1;
                }
            }
        }
    }
}
