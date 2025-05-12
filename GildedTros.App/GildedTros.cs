using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GildedTros.App
{
    public class GildedTros
    {
        IList<string> SmellyItems;
        IList<Item> Items;
        public GildedTros(IList<Item> Items)
        {
            this.Items = Items;
            SmellyItems = new List<string>();
            SmellyItems.Add("Duplicate Code");
            SmellyItems.Add("Long Methods");
            SmellyItems.Add("Ugly Variable Names");
        }

        private int AddQuality(Item item, int amountToAdd = 1)
        {
            int newQuality = item.Quality + amountToAdd;
            if (newQuality > 50)
                newQuality = 50;
            return newQuality;
        }

        private int RemoveQuality(Item item, int amount = 1)
        {
            int newItemQuality = item.Quality - amount;
            if (item.SellIn <= 0)
                newItemQuality = newItemQuality - amount;
            if (newItemQuality < 0)
                newItemQuality = 0;
            return newItemQuality;
        }

        public void UpdateQuality()
        {
            foreach (Item item in Items)
            {
                switch (item.Name) 
                {
                    case "Good Wine":
                        if (item.SellIn <= 0)
                            item.Quality = AddQuality(item, 2);
                        else
                            item.Quality = AddQuality(item);
                        break;
                    case "B-DAWG Keychain":
                        break;
                    case string n when item.Name.Contains("Backstage passes"):
                        switch (item.SellIn)
                        {
                            case <= 0:
                                item.Quality = 0;
                                break;
                            case < 6:
                                item.Quality = AddQuality(item, 3);
                                break;
                            case < 11:
                                item.Quality = AddQuality(item, 2);
                                break;
                            default:
                                item.Quality = AddQuality(item);
                                break;
                        }
                        break;
                    case string n when SmellyItems.Contains(item.Name):
                        item.Quality = RemoveQuality(item, 2);
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
