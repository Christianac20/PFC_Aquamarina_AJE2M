using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController_Triggers : MonoBehaviour
{
    #region VARIABLES
    public int scene;

    [Header("Variables de Componente y Scripts")]
    [SerializeField] Timer timer;
    [SerializeField] Animator animator;
    [SerializeField] SceneTransition sceneTransition;

    #endregion 

    #region METHODS
    // Awake is called when the script instance is being loaded
    void Awake()
    {
        timer = GetComponent<Timer>();
        animator = GetComponent<Animator>();
        sceneTransition = FindObjectOfType<SceneTransition>();
    }

    // Update is called once per frame
    void Update()
    {
        //If scene transition script isn´t assigned, find it and assign it
        if (sceneTransition == null)
        {
            sceneTransition = FindObjectOfType<SceneTransition>();
        }
    }

    #region TRIGGERS COLLISIONS CHECKING
    void OnTriggerEnter2D(Collider2D trigger)
    {
        //Detects if player gathers an air bubble
        if (trigger.gameObject.tag == ("AirBubble"))
        {
            timer.currentTime += timer.addAir;
            Destroy(trigger.gameObject);
        }

        //Detects if player takes damage
        if (trigger.gameObject.tag == ("Damage"))
        {
            timer.currentTime -= timer.depleteAir;
            animator.SetTrigger("DamageTaken");
        }

        //Detects if player touches a teleporter collider
        if (trigger.gameObject.tag == ("Teleporter"))
        {
            switch (trigger.name)
            {
                case "TP Scene 0":
                    scene = 0;
                    break;
                case "TP Scene 1":
                    scene = 1;
                    break;
                case "TP Scene 2":
                    scene = 2;
                    break;
                case "TP Scene 3":
                    scene = 3;
                    break;
                case "TP Scene 4":
                    scene = 4;
                    break;
            }

            Debug.Log("TP to scene en Script Triggers: " + scene); //Eliminado mientras parcheo de bug de TP
            sceneTransition.SceneChange();
        }
    }
    #endregion TRIGGERS COLLISIONS CHECKING

    #endregion METHODS
}
