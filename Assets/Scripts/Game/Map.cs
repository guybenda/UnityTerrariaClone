using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game
{
    public class Map
    {
        public readonly TileId[,] Tiles;
        public int Height { get; private set; }
        public int Width { get; private set; }

        public Map(int width, int height)
        {
            Tiles = new TileId[width, height];
            Height = height;
            Width = width;
        }

        public static Map GetDefaultMap()
        {
            Map map = new(30,20);

            for (int i = 0; i < map.Tiles.GetLength(0); i++)
            {
                map.Tiles[i, 0] = TileId.Stone;
                map.Tiles[i, 1] = TileId.Dirt;
            }

            return map;
        }
    }
}
