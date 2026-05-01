using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("Referencia al jugador")]
    public PlayerHealth playerHealth;

    [Header("Sprites de la barra de vida")]
    //[SerializeField] private Sprite[] healthBarSprites; // Array de 7 sprites (0% a 100%)
    public Sprite[] spritesHealthBar;

    [Header("Componente de imagen")]
    [SerializeField] private Image healthBarImage;

    void Start()
    {
        // Validaciones

        if (spritesHealthBar == null || spritesHealthBar.Length != 7)
        {
            Debug.LogError("Debes asignar exactamente 7 sprites en el array");
            return;
        }

        /*
        // Suscribirse al evento de cambio de vida
        playerHealth.OnHealthChanged.AddListener(UpdateHealthBar);

        // Actualizar la barra inicialmente
        UpdateHealthBar(playerHealth.GetCurrentHealth());
        */
    }

    private void Update()
    {
        switch (playerHealth.currentHealth)
        {
            case 6:
                healthBarImage.sprite = spritesHealthBar[6];
                break;
            case 5:
                healthBarImage.sprite = spritesHealthBar[5];
                break;
            case 4:
                healthBarImage.sprite = spritesHealthBar[4];
                break;
            case 3:
                healthBarImage.sprite = spritesHealthBar[3];
                break;
            case 2:
                healthBarImage.sprite = spritesHealthBar[2];
                break;
            case 1:
                healthBarImage.sprite = spritesHealthBar[1];
                break;
            case 0:
                healthBarImage.sprite = spritesHealthBar[0];
                break;
            default:
                print("esto ta roto eh");
                break;
        }
    }

    /* Metodos legacy
    public void UpdateHealthBar()
    {
        /* FUNCIONALIDAD DERECATED
        int maxHealth = playerHealth.GetMaxHealth();

        // Calcular el porcentaje de vida
        float healthPercentage = (float)currentHealth / maxHealth;

        // Convertir el porcentaje a índice del array (0-6)
        int spriteIndex = Mathf.RoundToInt(healthPercentage * 6);
        spriteIndex = Mathf.Clamp(spriteIndex, 0, 6);

        // Cambiar el sprite
        healthBarImage.sprite = healthBarSprites[spriteIndex];

        switch (playerHealth.currentHealth)
        {
            case 6:
                print("Salud del player 6 (switch)");
                healthBarImage.sprite = spritesHealthBar[6];
                break;
            case 5:
                print("Salud del player 5 (switch)");
                healthBarImage.sprite = spritesHealthBar[5];
                break;
            case 4:
                print("Salud del player 4 (switch)");
                healthBarImage.sprite = spritesHealthBar[4];
                break;
            case 3:
                print("Salud del player 3 (switch)");
                healthBarImage.sprite = spritesHealthBar[3];
                break;
            case 2:
                print("Salud del player 2 (switch)");
                healthBarImage.sprite = spritesHealthBar[2];
                break;
            case 1:
                print("Salud del player 1 (switch)");
                healthBarImage.sprite = spritesHealthBar[1];
                break;
            case 0:
                print("Salud del player 0 (switch)");
                healthBarImage.sprite = spritesHealthBar[0];
                break;
            default:
                print("esto ta roto eh");
                break;
        }
    }

    void OnDestroy()
    {
        // Desuscribirse del evento para evitar errores
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged.RemoveListener(UpdateHealthBar);
        }        
    }
    */
}