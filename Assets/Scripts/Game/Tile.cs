using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Game
{
    public enum TileId: byte
    {
        Air,
        Dirt,
        Stone,
    }

    public class Tile
    {
        public TileId id;
        public TileBase tile;

        public static readonly Tile[] Tiles;

        private static TileBase[] internalTiles;

        static Tile()
        {
            internalTiles = LoadTiles();
            Tiles = InitializeTiles();
        }

        private static Tile[] InitializeTiles()
        {
            return new Tile[]
            {
                new Tile
                {
                    id = TileId.Air,
                    tile = TileByName("Air"),
                },
                new Tile
                {
                    id = TileId.Dirt,
                    tile = TileByName("Dirt"),
                },
                new Tile
                {
                    id = TileId.Stone,
                    tile = TileByName("Stone"),
                }
            };
        }

        private static TileBase[] LoadTiles()
        {
            return Resources.LoadAll<TileBase>("Tiles");
        }
        private static TileBase TileByName(string name)
        {
            return internalTiles.Where(x => x.name == name).FirstOrDefault();
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
