using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] Animator canvasAnimator;

    [SerializeField] AnimationClip animacionFinal;
    [SerializeField] GameObject player;
    [SerializeField] GameObject canvasFades;
    [SerializeField] PlayerControllerWater playerControllerWater;
    [SerializeField] PlayerController_Ground playerControllerGround;
    [SerializeField] PlayerController_Triggers playerController_Triggers;

    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        canvasFades = GameObject.FindWithTag("PanelFades");
        canvasAnimator = canvasFades.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        playerControllerWater = FindObjectOfType<PlayerControllerWater>();
        playerControllerGround = FindObjectOfType<PlayerController_Ground>();
        playerController_Triggers = FindObjectOfType<PlayerController_Triggers>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasFades == null)
        {
            canvasFades = GameObject.FindWithTag("PanelFades");
        }
    }

    public void SceneChange()
    {
        StartCoroutine(ChangeScenePlayer());
    }

    IEnumerator ChangeScenePlayer()
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

        SceneManager.LoadScene(playerController_Triggers.sceneToTPPlayer);

        //muevo al player al punto de la pantalla en que quiero que aparezca
        //playerPositionOnEnter = GameObject.FindWithTag("PositionPlayerOnEntry");
        player.transform.position = playerController_Triggers.playerPositionOnEnter.transform.position;

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
