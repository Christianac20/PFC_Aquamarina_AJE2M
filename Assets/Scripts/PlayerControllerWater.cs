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

    InputAction actionMove;
    InputAction actionLook;
    InputAction actionRun;
    InputAction actionInteract;

    [Header("Variables generales")]
    public Vector2 moveAmmount;
    public float speedMultiplier = 1.0f;
    [SerializeField] float speed = 3;

    [Header("Variables del Animator")]
    [SerializeField] bool isAttacking;
    [SerializeField] bool isRunning;
    [SerializeField] bool movementY;
    bool _facingRight = false;

    [Header("Variables de Componente y Scripts")]
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigidbodyPlayer;
    [SerializeField] Timer timer;
    [SerializeField] FollowMouse followMouse;
    [SerializeField] PlayerController_SceneTypeChecker sceneTypeChecker;

    //[Header("Manejo de audio")]
    //public AudioManager audioManager;

    #endregion

    #region METHODS
    void Awake() //ASIGNO COMPONENTES Y ACTIONS
    {
        //ASIGNO LAS VARIABLES DE ACCIONES DEL INPUT SYSTEM
        actionMove = InputSystem.actions.FindAction("Move");
        actionLook = InputSystem.actions.FindAction("Look");
        actionRun = InputSystem.actions.FindAction("Run");
        actionInteract = InputSystem.actions.FindAction("Interact");

        //ASIGNO LAS VARIABLES DE COMPONENTES
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        timer = GetComponent<Timer>();
        animator = GetComponent<Animator>();
        sceneTypeChecker = GetComponent<PlayerController_SceneTypeChecker>();
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

        //CHECKING IF PLAYER DIED
        CheckDeath();

        //CHECKING IF GRAVITY IS RIGHT FOR THE LEVEL TYPE
        CheckGravity();

        //ANIMATOR VARIABLES SETTINGS
        animator.SetBool("Idle", moveAmmount == Vector2.zero);
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("Y Moving", movementY);
        animator.SetFloat("Y Movement", moveAmmount.y);
    }

    void FixedUpdate() //PHYSICS BASED METHODS CALLING
    {
        Swimming();
        Run();
        AnimationTagCheck();
    }

    void CheckDeath()
    {
        if (timer.currentTime <= 0)
        {
            animator.SetTrigger("Death");
            this.enabled = false;
        }
    }

    void CheckGravity()
    {
        if (rigidbodyPlayer.gravityScale != sceneTypeChecker.gravityWater)
        {
            rigidbodyPlayer.gravityScale = sceneTypeChecker.gravityWater;
        }
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
    public void Flip()
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

    #endregion METHODS
}