using Assets.Scripts.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    /** Track matrix changes with this collection
     * NOT thread-safe
     */
    public class TrackableMatrix<T>
    {
        private readonly T[,] data;

        public int X { get; private set; }
        public int Y { get; private set; }

        public readonly Queue<(int, int)> changes = new();

        public TrackableMatrix(int x, int y)
        {
            data = new T[x, y];
            X = x;
            Y = y;
        }

        public T this[int x, int y]
        {
            get { return data[x, y]; }
            set
            {
                if (x >= X || y >= Y || x < 0 || y < 0) return;

                changes.Enqueue((x, y));
                data[x, y] = value;
            }
        }

        public int GetLength(int dimension)
        {
            return data.GetLength(dimension);
        }

        public int Length { get { return data.Length; } }
    }

    public class TrackableTiles : TrackableMatrix<TileId>
    {
        public TrackableTiles(int x, int y) : base(x, y)
        {
        }
    }

    public static class ToupleExtentions
    {
        public static Vector3Int Vector(this (int, int) position)
        {
            return new(position.Item1, position.Item2);
        }
    }
}
