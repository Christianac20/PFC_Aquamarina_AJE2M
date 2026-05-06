using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    //El numero en "new GameObject[6]" indica el cupo de objetos que pasan al estado Don´tDestroy. Aumentar si se incluyen nuevos objetos
    [SerializeField] static GameObject[] persistentObjects = new GameObject[7]; 
    [SerializeField] int objectIndex;

    // Start is called before the first frame update
    void Awake()
    {
        if (persistentObjects[objectIndex] == null)
        {
            persistentObjects[objectIndex] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (persistentObjects[objectIndex] != gameObject)
        {
            Destroy(gameObject);
        }
    }
}
