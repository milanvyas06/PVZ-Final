using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantsVsZombies
{
    public class PotsManager : MonoBehaviour
    {
        #region PrivateVariables

        [SerializeField]
        private GameObject potPrefab;

        [SerializeField]
        private float tileSize = 1;

        private GameObject gridObject;

        private int rows = GameConstants.rows;
        private int columns = GameConstants.columns;
        private int skipColumn = GameConstants.skipColumn;

        #endregion /PrivateVariables

        #region MonobehaviourCallbacks

        private void Awake()
        {
            GeneratePotsGrid();
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

        private void GeneratePotsGrid()
        {
            float offsetLeft = (-columns / 2f) * tileSize + tileSize / 2f;
            float offsetBottom = (-rows / 2f) * tileSize + tileSize / 2f;

            // set it as first spawn position
            Vector3 nextPosition = new Vector3(offsetLeft, offsetBottom, 0f);

            gridObject = new GameObject("Pots");

            for (int r = 0; r < rows; r++)
            {
                GameObject rowObject = new GameObject($"Row {r + 1}");
                rowObject.transform.SetParent(gridObject.transform);

                for (int c = 0; c < columns; c++)
                {
                    if (c < skipColumn)
                    {
                        nextPosition.x += tileSize;
                        continue;
                    }

                    GameObject pot = Instantiate(potPrefab, Vector3.zero, Quaternion.identity, rowObject.transform);
                    pot.name = $"Pot {c + 1}";
                    pot.transform.position = nextPosition;
                    int currentRow = (rows - 1) - r;
                    int currentColumn = c;
                    pot.GetComponent<Pot>().SetGridItem(LevelManager.Instance.GetGridItemAt(currentRow, currentColumn));
                    nextPosition.x += tileSize + 0.2f;      // 0.2f is center to right
                }
                // reset x position and add y distance
                nextPosition.x = offsetLeft;
                nextPosition.y += tileSize;
            }
        }

        #endregion /PrivateMethods
    }
}
