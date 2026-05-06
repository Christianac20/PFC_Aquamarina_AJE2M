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
    public float scaredVelocity; //Velocidad en la que se mueve cuendo se asusta
    public float detectVelocity; //Velocidad límite para detectar al Player
    public PlayerControllerWater playerScript;
    [SerializeField] SpriteRenderer spriteRenderer;
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
            if (detectVelocity < playerScript.speedMultiplier) //Detecta si el multiplicador de velocidad del player es mayor que su límite de detección, en tal caso, se cumple el if
            {
                Debug.Log("Collision");
                agresive = true;
            }
        }
    }

    void Agresive()
    {
        Debug.Log("ÑomÑom");

    }
   /* public void TriggerEvent() //Se triggerea al final de la animación del pez huyendo
    {
        Destroy(this.gameObject);
    }
   */
}
