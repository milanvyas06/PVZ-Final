using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantsVsZombies
{
    public class YukiOnna : Enemy
    {
        #region PublicVariables

        public float Speed = 1f;
        public float AttackTime = 2f;
        public LayerMask rayLayerMask;

        #endregion /PublicVariables       

        #region PrivateVariables

        private bool useHit = false;
        private float attackTimer;

        private Weapon collideWeapon;
        private Pot collidePot;

        #endregion /PrivateVariables

        #region MonobehaviourCallbacks

        private void Start()
        {
            AnimState = EnemyState.Walk;
        }

        private void FixedUpdate()
        {

            ///NOT DESTROY POT OR ATTACK  ///LINE 154
            //RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, rayLayerMask);
            //if (hit2D.collider != null)
            //{
            //    if (hit2D.collider.gameObject.CompareTag(GameConstants.POT_TAG))
            //    {
            //        Debug.Log($"Hit");
            //        // StartCoroutine(Collide(collision));
            //        collidePot = hit2D.collider.gameObject.GetComponent<Pot>();
            //        //hit2D.collider.enabled = false;
            //        if (AnimState == EnemyState.Walk)
            //        {
            //            AnimState = EnemyState.Idle;
            //            AnimState = EnemyState.Attack;
            //        }
            //    }
            //}


            if (AnimState == EnemyState.Walk)
            {
                Move();
            }
            else
            {
                //Debug.Log($"Use Hit: {useHit}");

                if (useHit)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 2f, rayLayerMask);

                    if (hit.collider != null)
                    {
                        //Debug.Log($"Hit object: {hit2D.collider.gameObject.name}");

                        if (hit.collider.gameObject.CompareTag(GameConstants.WEAPON_TAG))
                        {
                            //Debug.Log($"{hit.collider.gameObject.name} Collides");
                            collideWeapon = hit.collider.gameObject.GetComponent<Weapon>();

                            if (attackTimer < Time.time)
                            {
                                AnimState = EnemyState.Attack;
                                attackTimer += AttackTime;
                            }
                        }
                    }
                    else
                    {
                        //Debug.Log($"Hit object: null");
                        collideWeapon = null;
                        useHit = false;
                        AnimState = EnemyState.Idle;
                        AnimState = EnemyState.Walk;
                    }

                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(GameConstants.WEAPON_TAG))
            {
                AnimState = EnemyState.Idle;
                attackTimer = Time.time;
                useHit = true;
                Color col;
                col.a = 0.75f;
                col.r = 0.75f;
                col.g = 0.75f;
                col.b = 0.75f;
                Debug.Log("Change Color 1 : " + col);
                gameObject.GetComponent<SpriteRenderer>().color = col;
                Invoke(nameof(ChangeColor), 0.2f);
            }

            else if (collision.gameObject.CompareTag(GameConstants.Bullet_TAG))
            {
                Color col;
                col.a = 0.75f;
                col.r = 0.75f;
                col.g = 0.75f;
                col.b = 0.75f;
                Debug.Log("Change Color 1 : " + col);
                gameObject.GetComponent<SpriteRenderer>().color = col;
                Invoke(nameof(ChangeColor), 0.2f);
            }

        }


        void ChangeColor()
        {
            Debug.Log("Change Color 2 : ");
            Color col2;
            col2.a = 1;
            col2.r = 1;
            col2.g = 1;
            col2.b = 1;
            gameObject.GetComponent<SpriteRenderer>().color = col2;
            AnimState = EnemyState.Walk;
        }


        private void OnDrawGizmos()
        {
            //if (useHit)
            Debug.DrawRay(transform.position, Vector2.left * 5f);
        }

        #endregion /MonobehaviourCallbacks

        #region Override_Methods

        public override void Attack()
        {
            base.Attack();

            //Color col;
            //col.a = 0.75f;
            //col.r = 0.75f;
            //col.g = 0.75f;
            //col.b = 0.75f;
            //Debug.Log("Change Color 1 : " + col);
            //gameObject.GetComponent<SpriteRenderer>().color = col;
            //Invoke(nameof(ChangeColor), 0.2f);

            //Check if weapon is ahead of me and reduce the health
            if (collideWeapon != null)
            {
                collideWeapon.GetHealth.TakeDamage(GetHealth.Damage);

                //Check if the weapon is died or not
                if (collideWeapon.GetHealth.PlayerHealth <= 0)
                {
                    Destroy(collideWeapon.gameObject);
                    useHit = false;
                    AnimState = EnemyState.Idle;
                    AnimState = EnemyState.Walk;
                }
            }

            ///NOT DESTROY POT OR ATTACK
            //if (collidePot != null)
            //{
            //    collidePot.OnPotClicked();
            //    collidePot = null;
            //    //AnimState = EnemyState.Idle;
            //    AnimState = EnemyState.Walk;
            //}
            //else
            //{
            //    //AnimState = EnemyState.Idle;
            //    AnimState = EnemyState.Walk;
            //}

        }

        public void Die_Destroy()
        {
            Destroy(this.gameObject);
        }

        public override void Move()
        {
            Body2d.MovePosition(Body2d.position + Vector2.left * Time.fixedDeltaTime * Speed);
        }

        #endregion /Override_Methods

        #region PrivateMethods

        #endregion /PrivateMethods
    }

}