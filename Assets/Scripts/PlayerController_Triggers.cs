using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController_Triggers : MonoBehaviour
{
    #region VARIABLES
    public int sceneToTPPlayer;

    [Header("Variables de Componente y Scripts")]
    [SerializeField] Timer timer;
    [SerializeField] Animator animator;
    [SerializeField] Animator bubblesDamageAnimator;
    [SerializeField] SceneTransition sceneTransition;

    [SerializeField] GameObject bubblesDamage;
    public GameObject playerPositionOnEnter;
    [SerializeField] GameObject[] playerPositionsArrayOnEnter;

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
            bubblesDamageAnimator.SetTrigger("Damage");
        }

        //Detects if player touches a teleporter collider
        if (trigger.gameObject.tag == ("Teleporter"))
        {
            switch (trigger.name)
            {
                case "TP Scene 0":
                    sceneToTPPlayer = 0;
                    playerPositionOnEnter = playerPositionsArrayOnEnter[0];
                    break;
                case "TP Scene 1L":
                    sceneToTPPlayer = 1;
                    playerPositionOnEnter = playerPositionsArrayOnEnter[1];
                    break;
                case "TP Scene 1R":
                    sceneToTPPlayer = 1;
                    playerPositionOnEnter = playerPositionsArrayOnEnter[2];
                    break;
                case "TP Scene 2":
                    sceneToTPPlayer = 2;
                    playerPositionOnEnter = playerPositionsArrayOnEnter[3];
                    break;
                case "TP Scene 3":
                    sceneToTPPlayer = 3;
                    playerPositionOnEnter = playerPositionsArrayOnEnter[4];
                    break;
                case "TP Scene 4":
                    sceneToTPPlayer = 4;
                    playerPositionOnEnter = playerPositionsArrayOnEnter[5];
                    break;
            }

            sceneTransition.SceneChange();
        }
    }
    #endregion TRIGGERS COLLISIONS CHECKING
    #endregion METHODS
}
