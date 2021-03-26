using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlantsVsZombies.Enums;
using System.Linq;

namespace PlantsVsZombies
{
    [CreateAssetMenu(fileName = "WeaponTypeSO", menuName = "ScriptableObject/WeaponTypeSO", order = 1)]
    public class WeaponTypeSO : ScriptableObject
    {
        public List<WeaponTypeSOItem> weaponTypeSOItems = new List<WeaponTypeSOItem>();

        public Sprite GetIdleSpriteOfType(WeaponType weaponType)
        {
            return weaponTypeSOItems.FirstOrDefault(s => s.weaponType == weaponType).idleSprite;
        }

        public GameObject GetDragObjectOfType(WeaponType weaponType)
        {
            return weaponTypeSOItems.FirstOrDefault((s) => s.weaponType == weaponType).dragObject;
        }

        public GameObject GetSpawnObjectOfType(WeaponType weaponType)
        {
            return weaponTypeSOItems.FirstOrDefault((s) => s.weaponType == weaponType).spawnObject;
        }
    }

    [System.Serializable]
    public class WeaponTypeSOItem
    {
        public WeaponType weaponType;
        public Sprite idleSprite;
        public GameObject dragObject;
        public GameObject spawnObject;
    }
}
