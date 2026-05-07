using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    #region VARIABLES
    //ITEM DATA
    [SerializeField] string itemName;
    [SerializeField] int quantity;
    [SerializeField] Sprite itemSprite;
    public bool isFull;

    //ITEM SLOT
    [SerializeField] TMP_Text quantityText;
    [SerializeField] Image itemImage;

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

    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
    }
    #endregion
}
