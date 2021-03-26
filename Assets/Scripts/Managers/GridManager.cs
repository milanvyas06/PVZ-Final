using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantsVsZombies
{
    public class GridManager : MonoBehaviour
    {
        #region PrivateVariables

        [SerializeField]
        private GameObject gridPrefab;

        [SerializeField]
        private float tileSize = 1;

        [SerializeField]
        private Sprite tileSprite1, tileSprite2;

        private GameObject gridObject;

        private int rows = GameConstants.rows;
        private int columns = GameConstants.columns;
        private int counter = 0;

        #endregion /PrivateVariables

        #region PublicVariables

        public static float XMin, YMin;

        #endregion /PublicVariables

        #region MonobehaviourCallbacks

        private void Awake()
        {
            counter = 0;
            GenerateGrid();
        }

        private void OnDisable()
        {
            if (gridObject != null)
            {
                Destroy(gridObject);
            }
        }

        #endregion /MonobehaviourCallbacks

        #region PrivateMethods

        private void GenerateGrid()
        {
            float offsetLeft = (-columns / 2f) * tileSize + tileSize / 2f;
            float offsetBottom = (-rows / 2f) * tileSize + tileSize / 2f;

            XMin = offsetLeft;  
            YMin = offsetBottom;

            // set it as first spawn position
            Vector3 nextPosition = new Vector3(offsetLeft, offsetBottom, 1f);

            gridObject = new GameObject("Grid");

            for (int r = 0; r < rows; r++)
            {
                GameObject rowObject = new GameObject($"Row {r + 1}");
                rowObject.transform.SetParent(gridObject.transform);

                for (int c = 0; c < columns; c++)
                {
                    GameObject tile = Instantiate(gridPrefab, rowObject.transform);
                    tile.GetComponent<SpriteRenderer>().sprite = counter % 2 == 0 ? tileSprite1 : tileSprite2;
                    tile.name = $"Tile {c + 1}";
                    tile.transform.position = nextPosition;
                    nextPosition.x += tileSize + 0.2f;  // 0.2f is center to right
                    counter++;
                }

                // reset x position and add y distance
                nextPosition.x = offsetLeft;
                nextPosition.y += tileSize;
            }
        }

        #endregion /PrivateMethods
    }
}
