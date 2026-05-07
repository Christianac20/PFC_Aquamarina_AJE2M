using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AtackCorrutine : MonoBehaviour
{
    #region variables
    int speed;
    public Transform[] points;
    NavMeshAgent agent;
    private int currentPosition = 0;
    public Transform tarject; //tarject es el Player
    public Rigidbody2D tarjectRigidbody;
    public bool agresive = false;
    public Vector2 distanceDifference;
    public float agresiveVelocity; //Velocidad en la que se mueve cuendo se asusta
    public float detectVelocity; //Velocidad límite para detectar al Player
    public PlayerControllerWater playerScript;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Vector2 launchToPlayerPosition;
    #endregion

    #region variables no usadas
    /*public float alfa;
      public float alfaMax = 1f;
      public float alfaMin = 0f;
      public float tiempoDesaparición = 1f;*/
    #endregion

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        #region Ajustes Iniciales
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //alfa = spriteRenderer.color.a;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(points[currentPosition].position);
        #endregion
    }

    void Update()
    {
        #region Animator Bools And Triggers

        animator.SetBool("Scared", agresive);

        #endregion

        if (agresive == false)
        {
            //Cambio de posición entre puntos
            if (!agent.pathPending && agent.remainingDistance <= 0.1)
            {
                currentPosition = (currentPosition + 1) % points.Length;
                agent.SetDestination(points[currentPosition].position);

             /*   
                if (spriteRenderer.flipX == false)
                {
                    spriteRenderer.flipX = true;
                }

                else if (spriteRenderer.flipX == true)
                {
                    spriteRenderer.flipX = false;
                }
             */
            }
        }

        else
        {
            Agresive();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            if (detectVelocity < playerScript.speedMultiplier) //Detecta si el multiplicador de velocidad del player es mayor que su límite de detección, en tal caso   , se cumple el if
            {
                Debug.Log("Collision");
                agresive = true;
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            agresive = false;
        }
    }

    void Agresive()
    {
        //Jesus, un 10 porfaporfi. No se lo digo a Dani porque me manda a la mierda.
        launchToPlayerPosition = tarject.transform.position;
        distanceDifference = (transform.position - tarject.position);
        Debug.Log("ÑomÑom");
        transform.Translate(launchToPlayerPosition * agresiveVelocity * Time.deltaTime);
    }
   /* public void TriggerEvent() //Se triggerea al final de la animación del pez huyendo
    {
        Destroy(this.gameObject);
    }
   */
}
