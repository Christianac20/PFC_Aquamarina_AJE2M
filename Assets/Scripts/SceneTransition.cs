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

    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        canvasFades = GameObject.FindWithTag("PanelFades");
        canvasAnimator = canvasFades.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
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

        yield return new WaitForSeconds(animacionFinal.length);

        SceneManager.LoadScene(scene);

        playerPositionOnEnter = GameObject.FindWithTag("PositionPlayerOnEntry");
        player.transform.position = playerPositionOnEnter.transform.position;
    }

    #endregion
}
