using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class BasicCorrutine : MonoBehaviour
{
    //variables
    int speed;
    public Transform[] points;
    NavMeshAgent agent;
    private int currentPosition = 0;
    public Transform tarject; //tarject es el Player
    public Rigidbody2D tarjectRigidbody;
    public bool scared = false;
    public Vector2 distanceDifference;
    public float scaredVelocity; //Velocidad en la que se mueve cuendo se asusta
    public int detectVelocity; //Velocidad límite para detectar al Player


    [SerializeField] SpriteRenderer spriteRenderer;
    public float alfa;
    public float alfaMax = 1f;
    public float alfaMin = 0f;
    public float tiempoDesaparición = 1f;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        alfa = spriteRenderer.color.a;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(points[currentPosition].position);

        animator.SetBool("Scared", scared);

    }

    void Update()
    {
        if (scared == false)
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
            Scared();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.tag == ("Player"))
       {
          // if (detectVelocity > tarjectRigidbody.velocity.magnitude)
          //{
                scared = true;
          //}
       }
    }

    void Scared()
    {
        //Huida
        distanceDifference = (transform.position - tarject.position).normalized;
        transform.Translate(distanceDifference * scaredVelocity * Time.deltaTime);
    }

    public void TriggerEvent()
    {
        Destroy(this.gameObject);
    }
}

