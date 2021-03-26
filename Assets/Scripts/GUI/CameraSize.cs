using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class CameraSize : MonoBehaviour
{

    void Awake()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
        {
            Application.targetFrameRate = 60;
            float aspect = (float)Screen.width / (float)Screen.height;
            aspect = (float)Math.Round(aspect, 2);
            Debug.Log("aspect : " + aspect);

            if (Screen.width >= 1280 && Screen.width <= 1400)
            {
                //GetComponent<Camera>().orthographicSize = 6f;               
                Debug.Log("Screen.width > 1280 : " + aspect);
            }

            if (aspect == 1.6f)
                GetComponent<Camera>().orthographicSize = 6f;                  //16:10
            else if (aspect == 1.78f)
                GetComponent<Camera>().orthographicSize = 6.2f;                  //16:9
            else if (aspect == 1.5f)
                GetComponent<Camera>().orthographicSize = 5.5f;                  //3:2
            else if (aspect == 1.33f)
                GetComponent<Camera>().orthographicSize = 5.2f;                  //4:3
            else if (aspect == 1.67f)
                GetComponent<Camera>().orthographicSize = 5f;                  //5:3
            else if (aspect == 1.25f)
                GetComponent<Camera>().orthographicSize = 5f;                  //5:4
            else if (aspect == 2.06f)
                GetComponent<Camera>().orthographicSize = 5f;                  //2960:1440
            else if (aspect == 2.17f)
                GetComponent<Camera>().orthographicSize = 5f;                  //iphone x
            else if (aspect == 2f)
                GetComponent<Camera>().orthographicSize = 5.5f;                  //OppoA73
        }

    }

}
