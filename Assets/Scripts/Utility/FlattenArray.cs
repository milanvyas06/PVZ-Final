using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace FlattenArray
{
    [System.Serializable]
    public class FlattenArray<T>
    {
        [SerializeField]
        private T[] array;

        private int rows;

        private int columns;

        public FlattenArray(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            array = new T[rows * columns];
        }

        public T this[int row, int column]
        {
            get => array[row + column * rows];
            set => array[row + column * rows] = value;
        }
    }
}
