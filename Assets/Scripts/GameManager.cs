using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region VARIABLES
    public GameObject[] keepObjects;
    public SceneTransition sceneTransition;

    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        sceneTransition = FindObjectOfType<SceneTransition>();

        for (int i = 0; i < keepObjects.Length; i++)
        {
            DontDestroyOnLoad(keepObjects[i]);
        }
        //DontDestroyOnLoad(keepObjects[i]);
    }
    #endregion
}
