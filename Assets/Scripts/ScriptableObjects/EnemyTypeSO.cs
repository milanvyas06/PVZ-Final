using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static PlantsVsZombies.Enums;

namespace PlantsVsZombies
{
    [CreateAssetMenu(fileName = "EnemyTypeSO", menuName = "ScriptableObject/EnemyTypeSO", order = 1)]
    public class EnemyTypeSO : ScriptableObject
    {
        public List<EnemyTypeSOItem> enemyTypeSOItems = new List<EnemyTypeSOItem>();

        public Sprite GetIdleSpriteOfType(EnemyType enemyType)
        {
            return enemyTypeSOItems.FirstOrDefault((s) => s.enemyType == enemyType).idleSprite;
        }

        public GameObject GetSpawnObjectOfType(EnemyType enemyType)
        {
            return enemyTypeSOItems.FirstOrDefault((s) => s.enemyType == enemyType).spawnObject;
        }
    }

    [System.Serializable]
    public class EnemyTypeSOItem
    {
        public EnemyType enemyType;
        public Sprite idleSprite;
        public GameObject spawnObject;
    }
}
