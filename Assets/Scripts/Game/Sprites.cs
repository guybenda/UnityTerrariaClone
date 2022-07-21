using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public enum SpriteId
    {
        Air,
        Dirt,
        Stone,
    }

    public class GameSprite
    {
        public readonly SpriteId id;
        public readonly Sprite sprite;
        public readonly Color color;

        public static readonly GameSprite[] Sprites;

        private static readonly Sprite[] InternalSprites;

        static GameSprite()
        {
            LoadSprites();
            InitializeSprites();
        }

        private GameSprite(SpriteId id, Sprite sprite, Color color)
        {
            this.id = id;
            this.sprite = sprite;
            this.color = color;
        }

        private static GameSprite[] InitializeSprites()
        {
            return new GameSprite[] {
                new GameSprite(SpriteId.Air, null, new Color(0f,0f,0f)),
                new GameSprite(SpriteId.Dirt, SpriteByName("Rough"), new Color(0.3301887f, 0.1829219f,0f)),
                new GameSprite(SpriteId.Stone, SpriteByName("Rough"), new Color(0.3490566f, 0.3490566f,0.3490566f))
            };
        }

        private static Sprite[] LoadSprites()
        {
            return Resources.LoadAll<Sprite>("TileSprites");

        }

        private static Sprite SpriteByName(string name)
        {
            return InternalSprites.Where(x => x.name == name).FirstOrDefault();
        }
    }


    public static class SpriteIdExtensions
    {
        public static GameSprite Sprite(this SpriteId id)
        {
            return GameSprite.Sprites[(int)id];
        }
    }
}
