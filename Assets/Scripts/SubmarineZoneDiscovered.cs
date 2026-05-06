using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmarineZoneDiscovered : MonoBehaviour
{
    #region VARIABLES
    bool zone0Discovered = true;
    bool zone1Discovered = false;
    bool zone2Discovered = false;
    [SerializeField] Button buttonZone0;
    [SerializeField] Button buttonZone1;
    [SerializeField] Button buttonZone2;

    #endregion

    #region METHODS
    // Awake
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SubmarineMapButtonsActivate();
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == ("SubmarineZone1"))
        {
            zone1Discovered = true;
        }

        if (trigger.gameObject.tag == ("SubmarineZone2"))
        {
            zone2Discovered = true;
        }
    }

    void SubmarineMapButtonsActivate()
    {
        if (zone1Discovered)
        {
            buttonZone1.enabled = true;
        }

        if (zone2Discovered)
        {
            buttonZone2.enabled = true;
        }
    }
    #endregion
}
