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
    bool _facingRight = false;

    [Header("Variables de Componente y Scripts")]
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigidbodyPlayer;
    [SerializeField] Timer timer;

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
    }

    void Update()
    {
        //Movement vector
        moveAmmount = actionMove.ReadValue<Vector2>();

        //CHECKING IF PLAYER DIED
        CheckDeath();

        //CHECKING IF GRAVITY IS RIGHT FOR THE LEVEL TYPE
        CheckGravity();

        //ANIMATOR VARIABLES SETTINGS
        animator.SetBool("Idle", moveAmmount == Vector2.zero);
    }

    void FixedUpdate() //PHYSICS BASED METHODS CALLING
    {
        Walking();
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
        if (rigidbodyPlayer.gravityScale != 1f)
        {
            rigidbodyPlayer.gravityScale = 1f;
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
    #endregion
}