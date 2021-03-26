using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlantsVsZombies.Enums;

namespace PlantsVsZombies
{
    public class Pot : MonoBehaviour
    {
        public GameObject potDestroy;
        public GameObject hammer;

        #region PrivateVariables

        private GridItem gridItem = null;

        #endregion /PrivateVariables

        #region MonobehaviourCallbacks

        public void OnMouseDown()
        {
            GameObject HammerPrefab =  Instantiate(hammer, transform.position, Quaternion.identity) as GameObject;
            HammerPrefab.transform.SetParent(this.transform);

            StartCoroutine(OnClickPot());
            //if (gridItem != null)
            //{
            //    if (gridItem.itemType == ItemType.Weapon)
            //    {
            //        GameObject cardObject = (GameObject)Instantiate(Resources.Load("Card"), transform.position, Quaternion.identity);
            //        cardObject.GetComponent<Card>().SetCardItem(gridItem.weaponType);
            //    }
            //    else if (gridItem.itemType == ItemType.Enemy)
            //    {
            //        //TODO: Init Zombie here
            //        GameObject enemyObject = LevelManager.Instance.GetSpawableEnemyOfType(gridItem.enemieType);
            //        Instantiate(enemyObject, transform.position, Quaternion.identity);
            //    }
            //
            //    Destroy(this.gameObject);
            //}
        }

        private IEnumerator OnClickPot()
        {
            yield return new WaitForSeconds(0.6f);
            Instantiate(potDestroy, transform.position, Quaternion.identity);
            if (gridItem != null)
            {
                if (gridItem.itemType == ItemType.Weapon)
                {
                    GameObject cardObject = (GameObject)Instantiate(Resources.Load("Card"), transform.position, Quaternion.identity);
                    cardObject.GetComponentInChildren<Card>().SetCardItem(gridItem.weaponType);
                }
                else if (gridItem.itemType == ItemType.Enemy)
                {
                    //TODO: Init Zombie here
                    GameObject enemyObject = LevelManager.Instance.GetSpawableEnemyOfType(gridItem.enemieType);
                    Instantiate(enemyObject, transform.position, Quaternion.identity);
                }

                Destroy(this.gameObject);
            }
        }

        public void OnPotClicked()
        {
            Instantiate(potDestroy, transform.position, Quaternion.identity);
            OnMouseDown();
        }

        #endregion /MonobehaviourCallbacks

        #region PublicMethods

        public void SetGridItem(GridItem gridItem)
        {
            this.gridItem = gridItem;
        }


        #endregion /PublicMethods
    }
}
