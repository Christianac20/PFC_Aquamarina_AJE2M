using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueTest : MonoBehaviour
{

    #region VARIABLES
    [Header("Dialogue Test Variables")]
    [SerializeField, TextArea(2, 4 )] private string[] dialogueLines;
    private bool isPlayerInRange;
    private bool didDialogueStart = false; // Variable para controlar si el diálogo está activo o no.
    [SerializeField] private int lineIndex = 0; // Índice de la línea de diálogo actual. 
    [SerializeField] private float typingTime = 0.05f;
    public PlayerControllerWater playerController; // Referencia al controlador del jugador, si es necesario para otras interacciones.
    [SerializeField] private int charsToPlayAudio; // Número de caracteres a escribir antes de reproducir el audio del NPC.
    [SerializeField] private bool isPlayerTalking = false;
    
    [Space]

    [Header("Dialogue Test References")]
    [SerializeField] private GameObject dialogueMark; // Referencia al objeto visual que se mostrará al jugador cuando esté en rango.
    [SerializeField] private GameObject dialoguePanel; // Referencia al panel de diálogo que se mostrará al jugador.
    [SerializeField] private TMP_Text dialogueText; // Referencia al cuadro de diálogo que se mostrará al jugador.
    [SerializeField] private AudioClip npcVoice; // Referencia al audio que se reproducirá durante el diálogo, si es necesario.
    [SerializeField] private AudioClip playerVoice; // Referencia al audio que se reproducirá durante el diálogo, si es necesario.
    [SerializeField] private AudioSource audioSource; // Referencia al audio que se reproducirá durante el diálogo, si es necesario.

    #endregion

    #region METHODS

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Obtiene el componente AudioSource del objeto actual.
        audioSource.clip = npcVoice; // Asigna el clip de audio del NPC al AudioSource.
    }

    // Update is called once per frame. Used to see what the player does each frame.
    void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine(); // Si el diálogo ya ha comenzado y la línea actual está completa, muestra la siguiente línea.
            }
            else
            {
                StopAllCoroutines(); // Si el jugador presiona F antes de que termine la línea actual, detiene la corrutina de escritura.
                dialogueText.text = dialogueLines[lineIndex]; // Muestra la línea completa inmediatamente.
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true); // Activa el panel de diálogo.
        dialogueMark.SetActive(false); // Desactiva el objeto visual de entrada.
        lineIndex = 0; // Reinicia el índice de la línea de diálogo actual.
        StartCoroutine(ShowLine()); // Inicia la corrutina para mostrar la primera línea de diálogo.
        //Time.timeScale = 0f; // Pausa el juego para que el jugador pueda leer el diálogo sin distracciones.
        playerController.enabled = false; // Desactiva el controlador del jugador para evitar movimientos durante el diálogo.

    }

    private void NextDialogueLine()
    {
        lineIndex++; // Incrementa el índice de la línea de diálogo actual.
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine()); // Muestra la siguiente línea de diálogo.
        }
        else
        {
            didDialogueStart = false; // Resetea la variable de control del diálogo.
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true); // Reactiva el objeto visual de entrada.
            //Time.timeScale = 1f; // Reanuda el juego una vez que se han mostrado todas las líneas de diálogo.
            playerController.enabled = true; // Reactiva el controlador del jugador para que pueda moverse nuevamente.
        }
    }

    private void SelectAudioClip()
    {
        if (lineIndex != 0)
        {
            isPlayerTalking = !isPlayerTalking; // Alterna el estado de si el jugador está hablando o no.
        }

        audioSource.clip = isPlayerTalking ? playerVoice : npcVoice; // Cambia el clip de audio según quién esté hablando. Es igual al bloque if comentado a continuación.

        /*if (isPlayerTalking)
        {
            audioSource.clip = playerVoice; // Cambia el clip de audio al del jugador si está hablando.
        }
        else
        {
            audioSource.clip = npcVoice; // Cambia el clip de audio al del NPC si no está hablando.
        }*/
    }

    private IEnumerator ShowLine()
    {
        SelectAudioClip(); // Selecciona el clip de audio correcto según quién esté hablando.
        dialogueText.text = string.Empty;
        int charIndex = 0; // Índice del carácter actual que se está mostrando.

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch; // Muestra cada carácter uno por uno.

            if(charIndex % charsToPlayAudio == 0) // Reproduce el audio del NPC cada 'charsToPlayAudio' caracteres.
            {
                audioSource.Play(); // Reproduce el clip de audio del NPC.
            }

            charIndex++;
            yield return new WaitForSecondsRealtime(typingTime); // Espera un poco antes de mostrar el siguiente carácter.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true); // Activa el objeto visual para indicar que el jugador está en rango.
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false); // Desactiva el objeto visual para indicar que el jugador ya no está en rango.
        }
    }

    #endregion
}