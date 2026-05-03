using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    #region VARIABLES
    public TextMeshProUGUI timerText; //ref al TextMeshPro del timer (test)

    public float totalTime = 120f; // Tiempo total del timer
    public float timeDecreaseSpeed = 1f; //Modificador de velocidad a la que disminuye el aire
    public float currentTime; //Cantidad de aire que tiene el player actualmente
    public float addAir = 20f; //Cantidad de aire que consigue el player al tomar burbujas
    public float depleteAir = 10f; //Cantidad de aire que pierde el player al recibir daño (a futuro variaría según origen del daño)
    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        currentTime = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Actualizar el contador
        currentTime -= Time.deltaTime * timeDecreaseSpeed;

        //Evitar que el contador baje de 0
        if (currentTime < 0f)
        {
            currentTime = 0f;
        }

        //Evitar que el contador suba de la capacidad máxima del tanque de 02
        if (currentTime > totalTime)
        {
            currentTime = totalTime;
        }

        //Metodo para actualizar el timer en pantalla
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        //Calcular minutos y segundos
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        //Formato de tiempo en 00:00
        string timeString = string.Format("{00:00}:{01:00}", minutes, seconds);

        //Update the UI text
        timerText.text = timeString;
    }
    #endregion
}
