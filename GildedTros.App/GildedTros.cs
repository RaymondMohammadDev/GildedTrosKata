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
            if (IsQualityUnder50(quality))
                return quality+amountToAdd;
            return quality;
        }

        private int RemoveQuality(int quality, int amountToRemove = 1)
        {
            if (IsQualityAbove0(quality))
                return quality - amountToRemove;
            return quality;
        }

        private bool IsQualityUnder50(int quality)
        {
            return quality < 50;
        }

        private bool IsQualityAbove0(int quality)
        {
            return quality > 0;
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
                    case var n when item.Name.Contains("Backstage passes"):
                        switch (item.SellIn)
                        {
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
                        if (item.Name != "B-DAWG Keychain")
                        {
                            item.Quality = RemoveQuality(item.Quality);
                        }
                        break;
                }

                if (item.Name != "B-DAWG Keychain")
                {
                    item.SellIn = item.SellIn - 1;
                }

                if (item.SellIn < 0)
                {
                    if (item.Name != "Good Wine")
                    {
                        if (item.Name != "Backstage passes for Re:factor"
                            && item.Name != "Backstage passes for HAXX")
                        {
                            if (item.Quality > 0)
                            {
                                if (item.Name != "B-DAWG Keychain")
                                {
                                    item.Quality = item.Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            item.Quality = item.Quality - item.Quality;
                        }
                    }
                    else
                    {
                        if (IsQualityUnder50(item.Quality))
                        {
                            item.Quality = item.Quality + 1;
                        }
                    }
                }
            }
        }
    }
}
