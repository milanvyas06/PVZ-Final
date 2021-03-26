using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantsVsZombies
{
    public class Arrow : Weapon
    {
        #region PublicVariables

        public float AttackTime = 2f;
        public LayerMask rayLayerMask;

        public Rigidbody2D arrowPrefeb;
        public Transform arrow;

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
            //if (useHit)
                Debug.DrawRay(transform.position, Vector2.right * 5f, Color.blue);
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
                    Destroy(collideEnemy.gameObject);
                    //ArrowAnimator.SetTrigger("Idle");
                    AnimState = WeaponState.Idle;
                    GameUIManager.Instance.CheckForGameWin();
                }
            }
        }

        #endregion /Override_Methods

        public float time = 0;

        protected readonly int ThrowStateHash = Animator.StringToHash("Throw");

        private void Update()
        {
            time += Time.deltaTime * 5;
            if (!useHit)
            {
                RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.right, 5f, rayLayerMask);
                if (hit2D.collider != null && time > 5)
                {
                    Animator2d.SetTrigger(ThrowStateHash);
                    time = 0;
                }
            }

        }


        private ArrowState animationState;

        protected ArrowState AnimStateArrow
        {
            get { return animationState; }
            set
            {
                animationState = value;
                ChangeAnimationState(animationState);
            }
        }

      
        private void ChangeAnimationState(ArrowState state)
        {
            switch (state)
            {
                case ArrowState.Throw:
                    Animator2d.SetTrigger(ThrowStateHash);
                    break;
              
            }
        }

        public void ThrowArrow()
        {
            Rigidbody2D instantiatedProjectile = Instantiate(arrowPrefeb, arrow.position, arrow.rotation) as Rigidbody2D;
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(6, 0, 0));
            AnimState = WeaponState.Idle;
        }

        protected enum ArrowState : byte
        {
            Throw = 2,
        }

    }

}


