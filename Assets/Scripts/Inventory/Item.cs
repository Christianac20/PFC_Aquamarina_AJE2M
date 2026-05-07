using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] string itemName;
    [SerializeField] int quantity;
    [SerializeField] Sprite sprite;

    [SerializeField] InventoryManager inventoryManager;

    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inventoryManager.AddItem(itemName, quantity, sprite);
            Destroy(gameObject);
        }
    }

    #endregion
}
