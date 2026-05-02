using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController_SceneTypeChecker : MonoBehaviour
{
    #region VARIABLES
    //Comprobaciones de Ground/Water para desactivar controles segun tipo de nivel
    public bool currentSceneIsGrounded;
    [SerializeField] int sceneIndex;
    [SerializeField] Scene currentScene;

    //Almacena referencias a los scripts de control de player para desactivarlos segun tipo de nivel
    [SerializeField] PlayerControllerWater playerControllerWater;
    [SerializeField] PlayerController_Ground playerControllerGround;

    #endregion

    #region METHODS
    // Awake is called when the script instance is being loaded
    void Awake()
    {
        playerControllerWater = GetComponent<PlayerControllerWater>();
        //playerControllerGround = GetComponent<PlayerController_Ground>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        currentScene = SceneManager.GetActiveScene(); //Obtiene la escena actual
        sceneIndex = currentScene.buildIndex; //Obtiene el indice de la escena actual (0 es la terrestre)

        if (sceneIndex == 0)
        {
            Debug.Log("playerControllerGround Activado");
            //playerControllerGround.enabled = true; //Activa el script de control terrestre
            playerControllerWater.enabled = false; //Desactiva el script de control acuático
        }
        else
        {
            Debug.Log("playerControllerGround Desactivado");
            //playerControllerGround.enabled = false; //Desactiva el script de control terrestre
            playerControllerWater.enabled = true; //Activa el script de control acuático
        }
        */
    }
    #endregion
}
