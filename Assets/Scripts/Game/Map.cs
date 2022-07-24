using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Map
    {
        public readonly TrackableTiles Tiles;


        public int Height { get; private set; }
        public int Width { get; private set; }

        public Map(int width, int height)
        {
            Tiles = new TrackableTiles(width, height);
            Height = height;
            Width = width;
        }

        public static Map GetDefaultMap()
        {
            Map map = new(60, 40);

            for (int i = 0; i < map.Tiles.GetLength(0); i++)
            {
                map.Tiles[i, 0] = TileId.Stone;
                map.Tiles[i, 1] = TileId.Stone;
                map.Tiles[i, 2] = TileId.Stone;
                map.Tiles[i, 3] = TileId.Stone;
                map.Tiles[i, 4] = TileId.Stone;
                map.Tiles[i, 5] = TileId.Stone;
                map.Tiles[i, 6] = TileId.Dirt;
                map.Tiles[i, 7] = TileId.Dirt;
                map.Tiles[i, 8] = TileId.Dirt;
                map.Tiles[i, 9] = TileId.Dirt;
            }

            return map;
        }
    }
}
