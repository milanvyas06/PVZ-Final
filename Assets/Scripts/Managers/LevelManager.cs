using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using static PlantsVsZombies.Enums;

namespace PlantsVsZombies
{
    public class LevelManager : MonoBehaviour
    {
        #region PublicVariables

        public static LevelManager Instance;

        #endregion /PublicVariables

        #region PrivateVarialbes

        private TextAsset levelsAsset;
        private WeaponTypeSO weaponTypeSO;
        private EnemyTypeSO enemyTypeSO;

        private int currentLevel = 0;
        private List<LevelItem> levelItems;

        #endregion /PrivateVarialbes

        #region MonobehaviourCallbacks

        private void Awake()
        {
            if (levelsAsset == null)
            {
                levelsAsset = Resources.Load<TextAsset>("Levels");
            }

            if (weaponTypeSO == null)
            {
                weaponTypeSO = Resources.Load<WeaponTypeSO>("SO/WeaponTypeSO");
            }

            if (enemyTypeSO == null)
            {
                enemyTypeSO = Resources.Load<EnemyTypeSO>("SO/EnemyTypeSO");
            }

            if (Instance == null)
            {
                Instance = this;
            }
        }

        #endregion /MonobehaviourCallbacks

        #region PublicMethods

        public GridItem GetGridItemAt(int row, int columns)
        {
            if (levelsAsset != null)
            {
                string data = levelsAsset.text;

                if (!string.IsNullOrEmpty(data))
                {
                    if (levelItems == null)
                    {
                        levelItems = JsonConvert.DeserializeObject<List<LevelItem>>(data);
                    }

                    return levelItems[currentLevel].levelGrid.gridItems[row][columns];
                }
            }

            return null;
        }

        public Sprite GetIdleSpriteOfWeaponType(WeaponType weaponType)
        {
            return weaponTypeSO.GetIdleSpriteOfType(weaponType);
        }

        public GameObject GetSpawableWeaponOfType(WeaponType weaponType)
        {
            return weaponTypeSO.GetSpawnObjectOfType(weaponType);
        }

        public GameObject GetDragableWeaponOfType(WeaponType weaponType)
        {
            return weaponTypeSO.GetDragObjectOfType(weaponType);
        }

        public GameObject GetSpawableEnemyOfType(EnemyType enemyType)
        {
            return enemyTypeSO.GetSpawnObjectOfType(enemyType);
        }

        #endregion /PublicMethods
    }
}
