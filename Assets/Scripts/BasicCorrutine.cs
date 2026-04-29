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
    public Transform tarject;
    private bool scared = false;
    private Vector2 distanceDifference;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(points[currentPosition].position);
    }

    void Update()
    {
        if (scared = false)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.1)
            {
                currentPosition = (currentPosition + 1) % points.Length;
                agent.SetDestination(points[currentPosition].position);

            }
        }
        Scared();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.tag == ("Player"))
       {
            scared = true;
       }
    }

    void Scared()
    {
        distanceDifference = (transform.position - tarject.position);
        transform.Translate(distanceDifference * Time.deltaTime);
    }
}

