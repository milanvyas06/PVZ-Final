using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantsVsZombies
{
    public class Samurai : Weapon
    {
        #region PublicVariables

        public float AttackTime = 2f;
        public LayerMask rayLayerMask;

        #endregion /PublicVariables 

        #region PrivateVariables

        private bool useHit = false;
        private float attackTimer;

        private Enemy collideEnemy;

        #endregion /PrivateVariables

        #region MonobehaviourCallbacks

        private void Start()
        {
            AnimState = WeaponState.Idle;
        }

        private void FixedUpdate()
        {
            if (useHit)
            {
                RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.right, 2f, rayLayerMask);

                if (hit2D.collider != null)
                {
                    //Debug.Log($"Hit object: {hit2D.collider.gameObject.name}");

                    if (hit2D.collider.gameObject.CompareTag(GameConstants.ENEMY_TAG))
                    {
                        //Debug.Log($"{hit2D.collider.gameObject.name} Collides");
                        collideEnemy = hit2D.collider.gameObject.GetComponent<Enemy>();

                        if (attackTimer < Time.time)
                        {
                            AnimState = WeaponState.Attack;
                            attackTimer += AttackTime;
                        }

                        return;
                    }
                }
                else
                {
                    //Debug.Log($"Hit object: null");
                    collideEnemy = null;
                    useHit = false;
                    AnimState = WeaponState.Idle;
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.Log($"{collision.gameObject.name}");

            if (collision.gameObject.CompareTag(GameConstants.ENEMY_TAG))
            {
                AnimState = WeaponState.Idle;
                attackTimer = Time.time;
                useHit = true;
            }
        }

        private void OnDrawGizmos()
        {
            if (useHit)
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

                //Check if the enemy is died or not
                if (collideEnemy.GetHealth.PlayerHealth <= 0)
                {
                    //Destroy(collideEnemy.gameObject);
                    collideEnemy.GetComponent<Animator>().Play("Die");
                    collideEnemy.GetComponent<BoxCollider2D>().enabled = false;
                    GameUIManager.Instance.CheckForGameWin();
                }
            }
        }

        #endregion /Override_Methods
    }
}
