using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public enum ItemId
    {

    }

    public struct Item
    {
        public readonly ItemId id;
        public readonly Sprite sprite;
        public readonly int maxStack;

        public static readonly Item[] Items;

        private static Sprite[] sprites;

        static Item()
        {
            sprites = LoadSprites();
            Items = InitializeItems();
        }

        private static Item[] InitializeItems()
        {
            return new Item[]
            {
                new Item
                {

                }
            };
        }

        private static Sprite[] LoadSprites()
        {
            return Resources.LoadAll<Sprite>("Items");
        }

        private static Sprite SpriteByName(string name)
        {
            return sprites.Where(x => x.name == name).FirstOrDefault();
        }
    }

    public static class ItemExtensions
    {
        public static Item Item(this ItemId id)
        {
            return Game.Item.Items[(int)id];
        }
    }
}
