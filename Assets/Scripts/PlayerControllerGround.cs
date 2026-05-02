using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerControllerGround : MonoBehaviour
{
    #region VARIABLES
    // Variables Float
    [Header("Variables Float")]
    public float horizontalInput; // Compartida -Input System
    public float verticalInput; // Compartida -Input System
    public float speedMultiplier = 1.0f; // Compartida
    public float xSpeed = 3; // Compartida
    public float ySpeed = 3; // Compartida
    public float gravityWater = 0f;
    public float gravityBase = 1f;

    // Variables Animator
    [Header("Variables del Animator")]
    bool isAttacking; // Compartida
    public bool isGrounded; 
    public bool isInWater; 
    bool isRunning; // Compartida
    bool _facingRight = true; // Compartida

    //Variables de Componente
    [Header("Variables de Componente")]
    public SpriteRenderer spriteRenderer;
    //public Animator animator;
    public Rigidbody2D rigidbodyPlayer;

    //Variables Compuestas
    [Header("Variables compuestas")]
    Vector2 movement;

    //Manejo de audio
    //public AudioManager audioManager;
    #endregion

    void Awake() //Usado para guardar componentes al iniciar
    {
        #region GUARDAR REFERENCIAS
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
        #endregion
    }

    void Update()
    {
        #region MOVEMENT
        // Movimiento lateral basico
        if (isAttacking == false)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal"); //Detecta cuando pulsas las flechas Izquierda / Derecha
            verticalInput = Input.GetAxisRaw("Vertical"); //Detecta cuando pulsas las flechas Izquierda / Derecha
            movement = new Vector2(horizontalInput, verticalInput); //variable de control para los idle

            transform.Translate(Vector2.right * xSpeed * speedMultiplier * horizontalInput * Time.deltaTime);
            if (!isGrounded) transform.Translate(Vector2.up * ySpeed * speedMultiplier * verticalInput * Time.deltaTime);

            // Flip character
            if (horizontalInput < 0f && _facingRight == true)
            {
                Flip();
            }
            else if (horizontalInput > 0f && _facingRight == false)
            {
                Flip();
            }
        }



        // Llamo a las funciones modificadoras del movimiento
        AnimationTagCheck();
        Run();
        AdaptGravity();
        #endregion

        #region ANIMATOR VARIABLES SET
        /*
        // Asigno variables a parametros del animator
        animator.SetBool("Idle", movement == Vector2.zero);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsRunning", isRunning);
        */
        #endregion 
    }

    #region METODOS MODIFICADORES DEL MOVIMIENTO

    //Corregimos la orientación del player
    private void Flip()
    {
        _facingRight = !_facingRight;
		float localScaleX = transform.localScale.x;
		localScaleX = localScaleX * -1f;
		transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    //Adaptar gravedad a estar dentro o fuera del agua
    private void AdaptGravity()
    {
        if (!isInWater)
        {
            rigidbodyPlayer.gravityScale = gravityBase;
        }
        else if (isInWater)
        {
            rigidbodyPlayer.gravityScale = gravityWater;
        }
    }

    // Nos aseguramos de que este pulsando o no el botón de correr
    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedMultiplier = 2f;
            isRunning = true;
        }
        else
        {
            speedMultiplier = 1.0f;
            isRunning = false;
        }
    }
    
    // Comprobacion de si está atacando
    private void AnimationTagCheck()
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
    #endregion

    #region ISGROUNDED CHECKING
    void OnCollisionEnter2D(Collision2D collision)
    {
        //movementScript.isGrounded = true;
        if (collision.gameObject.tag == ("Ground") || collision.gameObject.tag == ("Destructible"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.tag == ("Water"))
        {
            isInWater = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground") || collision.gameObject.tag == ("Destructible"))
        {
            isGrounded = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Water"))
        {
            isInWater = false;
        }
    }

    #endregion 
}