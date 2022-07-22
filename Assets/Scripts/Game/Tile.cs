using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public enum TileId
    {
        Air,
        Dirt,
        Stone,
    }

    public class Tile
    {
        public TileId id;
        public SpriteId sprite;
        public bool solid=true;

        public static readonly Tile[] Tiles = InitializeTiles();
        public static readonly Tile Default = Tiles[0];

        private static Tile[] InitializeTiles()
        {
            return new Tile[]
            {
                new Tile
                {
                    id = TileId.Air,
                    sprite = SpriteId.Air,
                    solid = false
                },
                new Tile
                {
                    id = TileId.Dirt,
                    sprite = SpriteId.Dirt
                },
                new Tile
                {
                    id = TileId.Stone,
                    sprite = SpriteId.Stone
                }
            };
        }


        public static Tile ById(TileId id)
        {
            return Tiles[(int)id];
            //return Tiles.Where(x => x.id == id).FirstOrDefault();
        }
    }

    public static class TileIdExtensions
    {
        public static Tile Tile(this TileId id)
        {
            return Game.Tile.Tiles[(int)id];
        }
    }
}
