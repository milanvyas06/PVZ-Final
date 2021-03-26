using UnityEngine;

namespace PlantsVsZombies
{
    public class Health : MonoBehaviour
    {
        #region Public_Variables

        public int Damage;

        #endregion /Public_Variables

        #region HealthProperty

        public float PlayerHealth { get; set; } = 100;

        #endregion /HealthProperty

        #region Public_Methods

        public void TakeDamage(float damage)
        {
            PlayerHealth -= damage;
        }

        #endregion /Public_Methods
    }
}
