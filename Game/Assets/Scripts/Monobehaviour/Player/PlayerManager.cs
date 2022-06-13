using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(PlayerCombat), typeof(SpriteRenderer))]
public class PlayerManager : MonoBehaviour
{
    public List<GameObject> inventory;
    public InventoryManager inventoryManager;
    SpriteRenderer sr { get { return GetComponent<SpriteRenderer>(); } }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) { ToggleInventory(); }
    }

    public void ToggleInventory(){ 
        foreach(GameObject ui in inventory){
            ui.SetActive(!ui.activeSelf);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Item")) {
            if(inventoryManager.AddItem(other.GetComponent<ItemInfo>().item)){
                Destroy(other.gameObject);
            }
        }
    }
}
