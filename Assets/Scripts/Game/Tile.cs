using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantsVsZombies
{
    public class Tile : MonoBehaviour
    {
        #region PublicVariables

        public bool IsOccupied { get; set; }

        #endregion /PublicVariables

        #region MonobehaviourCallbacks

        private void Update()
        {
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, -transform.forward);

            if (hit2D.collider != null)
            {
                if (hit2D.collider.gameObject.CompareTag(GameConstants.POT_TAG) ||
                   hit2D.collider.gameObject.CompareTag(GameConstants.CARD_TAG) ||
                   hit2D.collider.gameObject.CompareTag(GameConstants.WEAPON_TAG))
                {
                    IsOccupied = true;
                }
                else
                {
                    IsOccupied = false;
                }
            }

            //Debug.Log($"{gameObject.transform.parent.gameObject.name} X {gameObject.name} is Occupied == {IsOccupied}");
        }

        //private void OnDrawGizmos()
        //{
        //    Debug.DrawRay(transform.position, -transform.forward);
        //}

        #endregion /MonobehaviourCallbacks
    }
}
