using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    #region VARIABLES
    public TextMeshProUGUI timerText; //ref al TextMeshPro del timer (test)

    public float totalTime = 120f; // Tiempo total del timer
    public float timeDecreaseSpeed = 1;
    private float currentTime;

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
