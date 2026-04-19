using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.InputSystem;

public class PlayerControllerWater : MonoBehaviour
{
    #region VARIABLES
    [Header("Variables Input System")]
    [SerializeField] InputActionAsset inputActionAsset;

    [SerializeField] InputAction actionMove;
    [SerializeField] InputAction actionAttack;
    [SerializeField] InputAction actionRun;
    [SerializeField] InputAction actionEquipo1Camera;
    [SerializeField] InputAction actionEquipo2Net;
    [SerializeField] InputAction actionEquipo3NetLauncher;

    [Header("Variables generales")]
    [SerializeField] Vector2 moveAmmount;
    [SerializeField] float speedMultiplier = 1.0f;
    [SerializeField] float speed = 3;

    [Header("Variables del Animator")]
    [SerializeField] bool isAttacking;
    [SerializeField] bool isRunning;
    [SerializeField] bool movementY;
    [SerializeField] bool cameraEquipped;
    [SerializeField] bool cameraTakePhoto;
    [SerializeField] bool netEquipped;
    [SerializeField] bool netLauncherEquipped;
    bool _facingRight = false;

    [Header("Variables de Componente y Scripts")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigidbodyPlayer;
    [SerializeField] Timer timer;
    [SerializeField] SceneTransition sceneTransition;

    [Header("Otras Variables")]
    Vector2 movement;

    //[Header("Manejo de audio")]
    //public AudioManager audioManager;

    #endregion

    #region METHODS
    void Awake() //ASIGNO COMPONENTES Y ACTIONS
    {
        //ASIGNO LAS VARIABLES DE ACCIONES DEL INPUT SYSTEM
        actionMove = InputSystem.actions.FindAction("Move");
        actionRun = InputSystem.actions.FindAction("Run");
        actionAttack = InputSystem.actions.FindAction("Attack");
        actionEquipo1Camera = InputSystem.actions.FindAction("Equipo1_Camera");
        actionEquipo2Net = InputSystem.actions.FindAction("Equipo2_Net");
        actionEquipo3NetLauncher = InputSystem.actions.FindAction("Equipo3_NetLauncher");

        //ASIGNO LAS VARIABLES DE COMPONENTES
        sceneTransition = FindObjectOfType<SceneTransition>();
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = GetComponent<Timer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Movement vector
        moveAmmount = actionMove.ReadValue<Vector2>();

        //Checking Vertical Movement for animator state machine
        if (moveAmmount.y == 0)
        {
            movementY = false; 
        }
        else
        {
            movementY = true;
        }

        //If scene transition script isn´t assigned, find it and assign it
        if (sceneTransition == null)
        {
            sceneTransition = FindObjectOfType<SceneTransition>();
        }

        //EQUIPMENT FUNCTIONS
        CameraEquip();
        TakePhoto();
        NetEquip();
        NetLauncherEquip();

        //ANIMATOR VARIABLES SETTINGS
        animator.SetBool("Idle", moveAmmount == Vector2.zero);
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("Y Moving", movementY);
        animator.SetBool("CameraEquipped", cameraEquipped);
        animator.SetBool("NetEquipped", netEquipped);
        animator.SetBool("NetLauncherEquipped", netLauncherEquipped);
        animator.SetFloat("Y Movement", moveAmmount.y);
    }

    void FixedUpdate() //PHYSICS BASED METHODS CALLING
    {
        Swimming();
        Run();
        AnimationTagCheck();
    }

    #region MOVEMENT CONTROLS
    //MAIN BASIC MOVEMENT
    void Swimming()
    {
        if (isAttacking == false)
        {
            rigidbodyPlayer.velocity = new Vector2(moveAmmount.x * speed * speedMultiplier, moveAmmount.y * speed * speedMultiplier);

            //FLIP PLAYER
            if (moveAmmount.x < 0f && _facingRight == true)
            {
                Flip();
            }
            else if (moveAmmount.x > 0f && _facingRight == false)
            {
                Flip();
            }
        }
    }

    //FIX PLAYER ORIENTATION
    void Flip()
    {
        _facingRight = !_facingRight;
		float localScaleX = transform.localScale.x;
		localScaleX = localScaleX * -1f;
		transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    //RUN IF SPRINT BUTTON IS PRESSED
    private void Run()
    {
        if (actionRun.IsPressed())
        {
            speedMultiplier = 2f;
            isRunning = true;
            timer.timeDecreaseSpeed = 2f;
        }
        else
        {
            speedMultiplier = 1.0f;
            isRunning = false;
            timer.timeDecreaseSpeed = 1.0f;
        }
    }
    
    //CHECK IF PLAYER IS ATTACKING
    void AnimationTagCheck()
    {
        /*if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }*/
    }

    #endregion METODOS MODIFICADORES DEL MOVIMIENTO

    #region EQUIPMENT
    void CameraEquip()
    {
        if (actionEquipo1Camera.WasPressedThisFrame() && cameraEquipped == false)
        {
            cameraEquipped = true;
        }
        else if (actionEquipo1Camera.WasPressedThisFrame() && cameraEquipped == true)
        {
            cameraEquipped = false;
        }
        else if (moveAmmount != Vector2.zero)
        {
            cameraEquipped = false;
        }
    }

    void TakePhoto()
    {
        if (cameraEquipped && actionAttack.WasPressedThisFrame())
        {
            animator.SetTrigger("CameraTakePhoto");
        } 
    }

    void NetEquip()
    {
        if (actionEquipo2Net.WasPressedThisFrame())
        {
            netEquipped = true;
        }
    }

    void NetLauncherEquip()
    {
        if (actionEquipo3NetLauncher.WasPressedThisFrame())
        {
            netLauncherEquipped = true;
        }
    }
    #endregion

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
            Debug.Log("TP");
            sceneTransition.SceneChange();
        }
    }

    void OnTriggerExit2D(Collider2D trigger)
    {
        
    }
    #endregion TRIGGERS COLLISIONS CHECKING

    #endregion METHODS
}