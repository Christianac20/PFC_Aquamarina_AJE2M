using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class PlayerController_Ground : MonoBehaviour
{
    #region VARIABLES
    [Header("Variables Input System")]
    [SerializeField] InputActionAsset inputActionAsset;

    InputAction actionMove;

    [Header("Variables generales")]
    public Vector2 moveAmmount;
    [SerializeField] float speed = 3;

    // Variables Animator
    [Header("Variables del Animator")]
    bool isAttacking;

    [Header("Variables de Componente y Scripts")]
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigidbodyPlayer;
    [SerializeField] Timer timer;
    [SerializeField] PlayerController_SceneTypeChecker sceneTypeChecker;

    //Manejo de audio
    //public AudioManager audioManager;
    #endregion

    void Awake() //Usado para guardar componentes al iniciar
    {
        //ASIGNO LAS VARIABLES DE ACCIONES DEL INPUT SYSTEM
        actionMove = InputSystem.actions.FindAction("Move");

        rigidbodyPlayer = GetComponent<Rigidbody2D>(); // Compartida
        animator = GetComponent<Animator>();
        timer = GetComponent<Timer>();
        sceneTypeChecker = GetComponent<PlayerController_SceneTypeChecker>();
    }

    void Update()
    {
        //Movement vector
        moveAmmount = actionMove.ReadValue<Vector2>();

        //Checking if gravity is right for the level type
        CheckGravity();

        //CHECKING IF GRAVITY IS RIGHT FOR THE LEVEL TYPE
        CheckAir();

        //ANIMATOR VARIABLES SETTINGS
        animator.SetBool("Idle", moveAmmount == Vector2.zero);
    }

    void FixedUpdate() //PHYSICS BASED METHODS CALLING
    {
        Walking();
    }

    void CheckGravity()
    {
        if (rigidbodyPlayer.gravityScale != sceneTypeChecker.gravityGround)
        {
            rigidbodyPlayer.gravityScale = sceneTypeChecker.gravityGround;
        }
    }

    void CheckAir()
    {
        if (timer.currentTime != timer.totalTime)
        {
            timer.timeDecreaseSpeed = 0;
            timer.currentTime = timer.totalTime;
        }
    }


    #region MOVIMIENTO
    //MAIN BASIC MOVEMENT
    void Walking()
    {
        if (isAttacking == false)
        {
            rigidbodyPlayer.velocity = new Vector2(moveAmmount.x * speed, 0);

            //FLIP PLAYER
            if (moveAmmount.x < 0f && sceneTypeChecker.facingRight == true)
            {
                Flip();
            }
            else if (moveAmmount.x > 0f && sceneTypeChecker.facingRight == false)
            {
                Flip();
            }
        }
    }

    //FIX PLAYER ORIENTATION
    public void Flip()
    {
        sceneTypeChecker.facingRight =! sceneTypeChecker.facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    #endregion
}