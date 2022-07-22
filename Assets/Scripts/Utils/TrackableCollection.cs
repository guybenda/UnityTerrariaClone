﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utils
{
    public class TrackableMatrix<T>
    {
        private readonly T[,] data;

        public readonly Queue<(int, int)> changes = new();

        public TrackableMatrix(int x, int y)
        {
            data = new T[x, y];
        }

        public T this[int x, int y]
        {
            get { return data[x, y]; }
            set
            {
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
}