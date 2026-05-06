using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Submarine : MonoBehaviour
{
    #region VARIABLES
    [Header("Variables Input System")]
    [SerializeField] InputActionAsset inputActionAsset;

    InputAction actionInteract;

    [Header("Variables Generales")]
    [SerializeField] GameObject canvasFades;
    [SerializeField] Animator canvasAnimator;
    [SerializeField] AnimationClip animacionFinal;

    [SerializeField] GameObject player;
    int sceneToTPSubmarine;
    public string sceneToTPCode;
    bool isPlayerInSubmarineRange;
    bool isSubmarineMapOpen;
    [SerializeField] GameObject submarineMap;
    [SerializeField] GameObject submarinePositionOnEnter;
    [SerializeField] GameObject[] submarinePositionsArrayOnEnter;
    [SerializeField] PlayerControllerWater playerControllerWater;
    [SerializeField] PlayerController_Ground playerControllerGround;
    [SerializeField] PlayerController_SceneTypeChecker sceneTypeChecker;
    [SerializeField] GameObject submarineMark;

    #endregion

    #region VARIABLES
    void Awake()
    {
        //ASIGNO LAS VARIABLES DE ACCIONES DEL INPUT SYSTEM
        actionInteract = InputSystem.actions.FindAction("Interact");
        submarineMark = GameObject.FindWithTag("SubmarineRangeMark");
        submarineMark = GameObject.FindWithTag("SubmarineMap");
        canvasFades = GameObject.FindWithTag("PanelFades");
        canvasAnimator = canvasFades.GetComponent<Animator>();

        player = GameObject.FindWithTag("Player");
        playerControllerWater = FindObjectOfType<PlayerControllerWater>();
        playerControllerGround = FindObjectOfType<PlayerController_Ground>();
        sceneTypeChecker = FindObjectOfType<PlayerController_SceneTypeChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasAnimator == null)
        {
            canvasAnimator = GetComponentInChildren<Animator>();
        }
        if (submarineMap == null)
        {
            submarineMap = GameObject.FindWithTag("SubmarineMap");
        }

        if (isPlayerInSubmarineRange && actionInteract.WasPressedThisFrame())
        {
            submarineMap.SetActive(true);
            isSubmarineMapOpen = true; 
        }

        if (isSubmarineMapOpen == true)
        {
            playerControllerGround.enabled = false;
            playerControllerWater.enabled = false;
        }
        
        if (isSubmarineMapOpen == false && sceneTypeChecker.sceneIndex == 0)
        {
            playerControllerGround.enabled = true;
        }
        else if (isSubmarineMapOpen == false && sceneTypeChecker.sceneIndex != 0)
        {
            playerControllerWater.enabled = true;
        }

            switch (sceneToTPCode)
            {
                case "TP Scene 0":
                    sceneToTPSubmarine = 0;
                    submarinePositionOnEnter = submarinePositionsArrayOnEnter[0];
                    break;
                case "TP Scene 1":
                    sceneToTPSubmarine = 1;
                    submarinePositionOnEnter = submarinePositionsArrayOnEnter[1];
                    break;
                case "TP Scene 2":
                    sceneToTPSubmarine = 2;
                    submarinePositionOnEnter = submarinePositionsArrayOnEnter[2];
                    break;

            }
    }

    public void DefinirSceneToTPCode(string nuevoValor)
    {
        sceneToTPCode = nuevoValor;
        Debug.Log("Texto asignado: " + sceneToTPCode);
    }

    public void SceneTPSubmarine()
    {
        StartCoroutine(ChangeSceneSubmarine());
    }

    public void CloseSubmarineMap()
    {
        submarineMap.SetActive(false);
        isSubmarineMapOpen = false;
    }

    IEnumerator ChangeSceneSubmarine()
    {
        canvasAnimator.SetTrigger("Iniciar");

        //Desactivo los controles del player
        if (playerControllerWater != null)
        {
            playerControllerWater.enabled = false;
        }
        if (playerControllerGround != null)
        {
            playerControllerGround.enabled = false;
        }

        yield return new WaitForSeconds(animacionFinal.length);

        SceneManager.LoadScene(sceneToTPSubmarine);

        //muevo al player al punto de la pantalla en que quiero que aparezca
        //playerPositionOnEnter = GameObject.FindWithTag("PositionPlayerOnEntry");
        player.transform.position = submarinePositionOnEnter.transform.position;

        //Reactivo los controles del player
        if (playerControllerWater != null)
        {
            playerControllerWater.enabled = true;
        }
        if (playerControllerGround != null)
        {
            playerControllerGround.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInSubmarineRange = true;
            submarineMark.SetActive(true); // Activa el objeto visual para indicar que el jugador está en rango.
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInSubmarineRange = false;
            submarineMark.SetActive(false); // Desactiva el objeto visual para indicar que el jugador ya no está en rango.
        }
    }
    #endregion
}
