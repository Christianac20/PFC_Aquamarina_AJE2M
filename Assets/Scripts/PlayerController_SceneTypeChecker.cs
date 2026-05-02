using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.SceneManagement;

public class PlayerController_SceneTypeChecker : MonoBehaviour
{
    #region VARIABLES
    //Comprobaciones de Ground/Water para desactivar controles segun tipo de nivel
    public bool currentSceneIsGrounded;
    [SerializeField] int sceneIndex;
    [SerializeField] Scene currentScene;

    //Almacena referencias a los scripts de control de player para desactivarlos segun tipo de nivel y otros datos necesarios
    [SerializeField] Animator animator;
    [SerializeField] PlayerControllerWater playerControllerWater;
    [SerializeField] PlayerController_Equipment playerControllerEquipment;
    [SerializeField] PlayerController_Ground playerControllerGround;
    [SerializeField] AnimatorController[] animatorControllers;

    #endregion

    #region METHODS
    // Awake is called when the script instance is being loaded
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerControllerWater = GetComponent<PlayerControllerWater>();
        //playerControllerGround = GetComponent<PlayerController_Ground>();
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene(); //Obtiene la escena actual
        sceneIndex = currentScene.buildIndex; //Obtiene el indice de la escena actual (0 es la terrestre)

        if (sceneIndex == 0)
        {
            //playerControllerGround.enabled = true; //Activa el script de control terrestre
            //playerControllerWater.enabled = false; //Desactiva el script de control acuático
            playerControllerEquipment.enabled = false; //Desactiva el script de uso de equipamientos
            animator.runtimeAnimatorController = animatorControllers[0];
        }
        else
        {
            //playerControllerGround.enabled = false; //Desactiva el script de control terrestre
            //playerControllerWater.enabled = true; //Activa el script de control acuático
            playerControllerEquipment.enabled = true; //Activa el script de uso de equipamientos
            animator.runtimeAnimatorController = animatorControllers[1];
        }
    }
    #endregion
}
