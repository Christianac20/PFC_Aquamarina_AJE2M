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
        if (scared != true)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.1)
            {
                currentPosition = (currentPosition + 1) % points.Length;
                agent.SetDestination(points[currentPosition].position);

            }
        }

    }

        /*
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("tontaco");
            if (collision.gameObject.tag  == ("Player"))
            {
               transform.Translate(new Vector2((tarject.position.x)-(transform.position.x), (tarject.position.x)-(transform.position.y)));
            }
        }
        */
}

