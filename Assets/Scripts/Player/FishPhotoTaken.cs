using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPhotoTaken : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] bool pezPayaso = false;
    [SerializeField] bool caballitoDeMar = false;
    [SerializeField] bool pezGlobo = false;
    [SerializeField] bool pezCirujanoAzul = false;
    [SerializeField] bool anchoa = false;
    [SerializeField] bool medusaLuna = false;
    [SerializeField] bool raya = false;
    [SerializeField] bool pezCometa = false;
    [SerializeField] bool atun = false;
    [SerializeField] bool barracuda = false;
    [SerializeField] bool delfin = false;
    [SerializeField] bool tortugaBoba = false;
    [SerializeField] bool tiburonBlanco = false;
    [SerializeField] bool pezLuna = false;
    [SerializeField] bool orca = false;
    [SerializeField] bool angelMarino = false;
    [SerializeField] bool pezHacha = false;
    [SerializeField] bool gamba = false;
    [SerializeField] bool pezFoco = false;
    [SerializeField] bool pezCristal = false;
    [SerializeField] bool pulpo = false;
    [SerializeField] bool calamar = false;
    [SerializeField] bool pezComecocos = false;
    [SerializeField] bool anguilaPelicano = false;
    [SerializeField] bool pezBala = false;
    [SerializeField] bool pezLinterna = false;
    [SerializeField] bool pezDinosaurio = false;
    [SerializeField] bool pezGato = false;
    [SerializeField] bool pezLaboratorio = false;

    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        //Detects if player gathers an air bubble
        switch (trigger.gameObject.tag)
        {
            case "PezPayaso":
                pezPayaso = true;
                break;

            case "CaballitoDeMar":
                caballitoDeMar = true;
                break;

            case "PezGlobo":
                pezGlobo = true;
                break;

            case "PezCirujanoAzul":
                pezCirujanoAzul = true;
                break;

            case "Anchoa":
                anchoa = true;
                break;

            case "MedusaLuna":
                medusaLuna = true;
                break;

            case "Raya":
                raya = true;
                break;

            case "PezCometa":
                pezCometa = true;
                break;

            case "Atun":
                atun = true;
                break;

            case "Barracuda":
                barracuda = true;
                break;

            case "TortugaBoba":
                tortugaBoba = true;
                break;

            case "TiburonBlanco":
                tiburonBlanco = true;
                break;

            case "PezLuna":
                pezLuna = true;
                break;

            case "Orca":
                orca = true;
                break;

            case "AngelMarino":
                angelMarino = true;
                break;

            case "PezHacha":
                pezHacha = true;
                break;

            case "Gamba":
                gamba = true;
                break;

            case "PezFoco":
                pezFoco = true;
                break;

            case "PezCristal":
                pezCristal = true;
                break;

            case "Pulpo":
                pulpo = true;
                break;

            case "Calamar":
                calamar = true;
                break;

            case "PezComecocos":
                pezComecocos = true;
                break;

            case "AnguilaPelicano":
                anguilaPelicano = true;
                break;

            case "PezBala":
                pezBala = true;
                break;

            case "PezLlinterna":
                pezLinterna = true;
                break;

            case "PezDinosaurio":
                pezDinosaurio = true;
                break;

            case "PezGato":
                pezGato = true;
                break;

            case "PezLaboratorio":
                pezLaboratorio = true;
                break;


        }

        if (trigger.gameObject.tag == ("Fih1"))
        {
            
        }
    }
    #endregion
}
