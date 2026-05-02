using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    #region VARIABLES
    Animator canvasAnimator;
    public string scene;
    [SerializeField] AnimationClip animacionFinal;
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerPositionOnEnter;
    [SerializeField] GameObject canvasFades;
    [SerializeField] PlayerControllerWater playerControllerWater;
    [SerializeField] PlayerControllerGround playerControllerGround;

    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        canvasFades = GameObject.FindWithTag("PanelFades");
        canvasAnimator = canvasFades.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        playerControllerWater = FindObjectOfType<PlayerControllerWater>();
        playerControllerGround = FindObjectOfType<PlayerControllerGround>();
    }

    // Update is called once per frame
    void Update()
    {
        /*//Cambio de escena por tecla. Test Inicial. Deprecated
        if (Input.GetKeyUp(KeyCode.T))
        {
            StartCoroutine(ChangeScene());
        }
        */
    }

    public void SceneChange()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
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

        SceneManager.LoadScene(scene);

        //muevo al player al punto de la pantalla en que quiero que aparezca
        playerPositionOnEnter = GameObject.FindWithTag("PositionPlayerOnEntry");
        player.transform.position = playerPositionOnEnter.transform.position;

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

    #endregion
}
