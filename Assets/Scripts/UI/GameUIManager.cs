using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlantsVsZombies
{
    public class GameUIManager : MonoBehaviour
    {
        #region Public_Variables

        public static GameUIManager Instance;

        public GameObject GameoverCanvas;
        public GameObject GamewinCanvas;

        #endregion /Public_Variables

        #region MonobehaviourCallbacks

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        #endregion /MonobehaviourCallbacks

        #region Public_Methods

        public void ReloadScene()
        {
            SceneManager.LoadScene(0);
        }

        int totalEnemies;

        public void CheckForGameWin()
        {
            totalEnemies++;
            Debug.Log("totalEnemies : " + totalEnemies);

            if (IsGameWin())
            {
                GamewinCanvas.SetActive(true);
            }
        }

        public bool IsGameWin()
        {
            return totalEnemies == 15;
        }

        #endregion /Public_Methods
    }
}
