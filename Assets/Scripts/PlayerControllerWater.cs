using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerControllerWater : MonoBehaviour
{
    #region VARIABLES
    // Variables Float
    [Header("Variables Float")]
    public float horizontalInput;
    public float verticalInput;
    public float speedMultiplier = 1.0f;
    public float xSpeed = 3;
    public float ySpeed = 3;

    // Variables Animator
    [Header("Variables del Animator")]
    bool isAttacking;
    public bool isGrounded;
    public bool isInWater;
    bool isRunning;
    private bool _facingRight = true;

    //Variables de Componente
    [Header("Variables de Componente")]
    public SpriteRenderer spriteRenderer;
    //public Animator animator;
    public Rigidbody2D rigidbodyPlayer;

    //Otras Variables
    [Header("Otras Variables")]
    Vector2 movement;
    [SerializeField] Timer timer;
    [SerializeField] SceneTransition sceneTransition;

    //Manejo de audio
    //public AudioManager audioManager;
    #endregion

    void Start() //Usado para guardar componentes al iniciar
    {
        #region GUARDAR REFERENCIAS
        sceneTransition = FindObjectOfType<SceneTransition>();
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = GetComponent<Timer>();
        //animator = GetComponent<Animator>();
        #endregion
    }

    void Update()
    {
        if (sceneTransition == null)
        {
            sceneTransition = FindObjectOfType<SceneTransition>();
        }

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

    // Nos aseguramos de que este pulsando o no el botón de correr
    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
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
        
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        //Detecta colision de entrada con agua
        if (trigger.gameObject.tag == ("Water"))
        {
            isInWater = true;
        }

        //Detecta colision de entrada con un trigger de aire
        if (trigger.gameObject.tag == ("AirBubble")) 
        {
            timer.currentTime += timer.addAir;
            Destroy(trigger.gameObject);
        }

        //Detecta colision de entrada con un trigger de daño
        if (trigger.gameObject.tag == ("Damage"))
        {
            timer.currentTime -= timer.depleteAir;
        }

        //Detecta colision de entrada con un trigger de cambio de escena
        if (trigger.gameObject.tag == ("Teleporter"))
        {
            Debug.Log("TP");
            sceneTransition.SceneChange();
        }
    }

    void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == ("Water"))
        {
            isInWater = false;
        }
    }

    #endregion
}