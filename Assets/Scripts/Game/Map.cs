using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game
{
    public class MapTiles
    {
        private TileId[,] _tiles;

        public TileId this[int w, int h]
        {
            get { return _tiles[w, h]; }
            set
            {
                _tiles[w, h] = value;
                // TODO implement updates queue
            }
        }
    }

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
            Map map = new(120, 80);

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
