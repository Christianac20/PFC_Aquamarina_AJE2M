using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region VARIABLES

    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (this.transform.position != Vector3.zero)
        {
            this.transform.position = Vector3.zero;
        }
    }
    #endregion
}
