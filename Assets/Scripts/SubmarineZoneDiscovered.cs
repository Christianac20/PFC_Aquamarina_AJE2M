using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmarineZoneDiscovered : MonoBehaviour
{
    #region VARIABLES
    //VARIABLES DE COMPROBACION DE ZONAS DESCUBIERTAS
    public bool zone0Discovered = true;
    public bool zone1Discovered = false;
    public bool zone2Discovered = false;

    //VARIABLES DE ACTIVACION DE TRIGGERS DE DESCUBRIR ZONAS
    [SerializeField] Collider2D zone1Discover;
    [SerializeField] Collider2D zone2Discover;

    //VARIABLES DE SCRIPTS Y COMPONENTES
    [SerializeField] PlayerController_SceneTypeChecker sceneTypeChecker;
    #endregion

    #region METHODS
    // Awake
    void Awake()
    {
        sceneTypeChecker = GetComponent<PlayerController_SceneTypeChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        SubmarineDiscoverZoneTriggers();
    }

    void SubmarineDiscoverZoneTriggers()
    {
        switch (sceneTypeChecker.sceneIndex)
        {
            case 1:
                zone1Discover.enabled = true;
                zone2Discover.enabled = false;
                break;
            case 2:
                zone1Discover.enabled = false;
                zone2Discover.enabled = true;
                break;
        }
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
    #endregion
}
