using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlantsVsZombies
{
    public class GameoverCollider : MonoBehaviour
    {
        #region MonobehaviourCallbacks

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(GameConstants.ENEMY_TAG))
            {
                GameUIManager.Instance.GameoverCanvas.SetActive(true);
            }
        }

        #endregion /MonobehaviourCallbacks


        public void Retry()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

}
