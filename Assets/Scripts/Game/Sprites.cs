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
        Dirt,
        Stone,
    }

    public class GameSprite
    {
        public readonly SpriteId id;
        public readonly Texture2D texture;
        public readonly Color color;

        public static readonly GameSprite[] Sprites;

        private static readonly Texture2D[] InternalTextures;

        static GameSprite()
        {
            LoadSprites();
            InitializeSprites();
        }

        private GameSprite(SpriteId id, Texture2D texture, Color color)
        {
            this.id = id;
            this.texture = texture;
            this.color = color;
        }

        private static GameSprite[] InitializeSprites()
        {
            return new GameSprite[] {
                new GameSprite(SpriteId.Dirt, TextureByName("Rough"), new Color(0.3301887f, 0.1829219f,0f)),
                new GameSprite(SpriteId.Stone, TextureByName("Rough"), new Color(0.3490566f, 0.3490566f,0.3490566f))
            };
        }

        private static Texture2D[] LoadSprites()
        {
            return Resources.LoadAll<Texture2D>("TileSprites");

        }

        private static Texture2D TextureByName(string name)
        {
            return InternalTextures.Where(x => x.name == name).FirstOrDefault();
        }


        public static GameSprite ById(SpriteId id)
        {
            return Sprites.Where(x => x.id == id).FirstOrDefault();
        }
    }
}
