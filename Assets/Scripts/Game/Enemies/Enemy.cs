using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantsVsZombies
{
    public class Enemy : MonoBehaviour
    {
        #region Public_Variables

        private Rigidbody2D _body;

        public Rigidbody2D Body2d
        {
            get
            {
                if (_body == null)
                {
                    _body = GetComponent<Rigidbody2D>();
                }

                return _body;
            }
        }

        private Animator _animator;

        public Animator Animator2d
        {
            get
            {
                if (_animator == null)
                {
                    _animator = GetComponent<Animator>();
                }

                return _animator;
            }
        }

        private EnemyState _animationState;

        public EnemyState AnimState
        {
            get { return _animationState; }
            set
            {
                _animationState = value;
                ChangeAnimationState(_animationState);
            }
        }

        private Health _health;

        public Health GetHealth
        {
            get
            {
                if (_health == null)
                {
                    _health = GetComponent<Health>();
                }

                return _health;
            }
        }

        #endregion /Public_Variables

        #region PrivateVariables

        protected readonly int idleStateHash = Animator.StringToHash("Idle");
        protected readonly int walkStateHash = Animator.StringToHash("Walk");
        protected readonly int attackStateHash = Animator.StringToHash("Attack");

        #endregion /PrivateVariables

        #region Virtual_Methods

        public virtual void Move() { }

        public virtual void Attack() { }

        #endregion /Virtual_Methods

        #region PrivateMethods

        private void ChangeAnimationState(EnemyState state)
        {
            switch (state)
            {
                case EnemyState.Idle:
                    Animator2d.SetTrigger(idleStateHash);
                    break;
                case EnemyState.Walk:
                    Animator2d.SetTrigger(walkStateHash);
                    break;
                case EnemyState.Attack:
                    Animator2d.SetTrigger(attackStateHash);
                    break;
            }
        }

        #endregion /PrivateMethods

        #region Enums


        public enum EnemyState : byte
        {
            Idle = 0,
            Walk,
            Attack
        }

        #endregion /Enums
    }
}
