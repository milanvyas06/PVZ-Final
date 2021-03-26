using System.Collections.Generic;
using UnityEngine;
using static PlantsVsZombies.Enums;
using FlattenArray;
using Newtonsoft.Json;

namespace PlantsVsZombies
{
    [System.Serializable]
    public class LevelItem
    {
        public LevelGrid levelGrid;
        public bool foldOut;

        public LevelItem(int rows, int columns, int skipColumn)
        {
            levelGrid = new LevelGrid(rows, columns, skipColumn);
        }
    }

    [System.Serializable]
    public class LevelGrid
    {
        public LevelGrid(int rows, int columns, int skipColumn)
        {
            //gridItems = new FlattenArrayLevelGridSerializable(rows, columns);
            gridItems = new List<List<GridItem>>();

            for (int i = 0; i < rows; i++)
            {
                gridItems.Add(new List<GridItem>());

                for (int j = 0; j < columns; j++)
                {
                    if (j < skipColumn)
                    {
                        gridItems[i].Add(null);
                    }
                    else
                    {
                        gridItems[i].Add(new GridItem());
                    }
                }
            }
        }

        [SerializeField]
        public List<List<GridItem>> gridItems;

        //[SerializeField]
        //public FlattenArrayLevelGridSerializable gridItems;
    }

    [System.Serializable]
    public class FlattenArrayLevelGrid : FlattenArray<GridItem>
    {
        public FlattenArrayLevelGrid(int rows, int columns) : base(rows, columns)
        {
        }
    }

    [System.Serializable]
    public class FlattenArrayLevelGridSerializable
    {
        [SerializeField]
        private FlattenArrayLevelGrid serializableArray;

        public FlattenArrayLevelGridSerializable(int rows, int columns)
        {
            serializableArray = new FlattenArrayLevelGrid(rows, columns);
        }

        public GridItem this[int row, int columns]
        {
            get => serializableArray[row, columns];
            set => serializableArray[row, columns] = value;
        }
    }

    [System.Serializable]
    public class GridItem
    {
        public ItemType itemType;
        public WeaponType weaponType;
        public EnemyType enemieType;
    }
}