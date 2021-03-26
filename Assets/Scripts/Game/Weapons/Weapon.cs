using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlantsVsZombies
{
    public class Weapon : MonoBehaviour
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

        private WeaponState _animationState;

        protected WeaponState AnimState
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
        protected readonly int attackStateHash = Animator.StringToHash("Attack");

        #endregion /PrivateVariables

        #region Virtual_Methods

        public virtual void Attack() { }

        #endregion /Virtual_Methods

        #region PrivateMethods

        private void ChangeAnimationState(WeaponState state)
        {
            switch (state)
            {
                case WeaponState.Idle:
                    Animator2d.SetTrigger(idleStateHash);
                    break;
                case WeaponState.Attack:
                    Animator2d.SetTrigger(attackStateHash);
                    break;
            }
        }

        #endregion /PrivateMethods

        #region Enums

        protected enum WeaponState : byte
        {
            Idle = 0,
            Attack = 1,
        }

        #endregion /Enums
    }
}