using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantsVsZombies
{
    public class ArrowDamage : Weapon
    {
        #region PublicVariables

        public float AttackTime = 1f;
        public LayerMask rayLayerMask;

        #endregion /PublicVariables 

        #region PrivateVariables

        private float attackTimer;

        private Enemy collideEnemy;

        #endregion /PrivateVariables

        #region MonobehaviourCallbacks



        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(GameConstants.ENEMY_TAG))
            {
                collideEnemy = collision.gameObject.GetComponent<Enemy>();
                Attack();
                Destroy(this.gameObject);
                attackTimer = Time.time;
            }
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(transform.position, Vector2.right * 2f, Color.blue);
        }


        #endregion /MonobehaviourCallbacks


        #region Override_Methods

        public override void Attack()
        {
            base.Attack();
            //TODO: Check if enemy is ahead of me and reduce the health
            if (collideEnemy != null)
            {
                collideEnemy.GetHealth.TakeDamage(GetHealth.Damage);
                //collideEnemy.GetComponent<SpriteRenderer>().color = Color.red;

                //Check if the enemy is died or not
                if (collideEnemy.GetHealth.PlayerHealth <= 0)
                {
                    //Destroy(collideEnemy.gameObject);
                    GameUIManager.Instance.CheckForGameWin();
                    collideEnemy.GetComponent<Animator>().Play("Die");
                    collideEnemy.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }

        #endregion /Override_Methods


    }
}
