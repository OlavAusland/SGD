using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Weapon weapon;
    public List<GameObject> inventory;
    public InventoryManager inventoryManager;
    

    public float speed = 12f;
    Rigidbody2D rb { get { return GetComponent<Rigidbody2D>(); } }
    
    public void Update()
    {
        Movement();
        Combat();
        if (Input.GetKeyDown(KeyCode.Escape)) { ToggleInventory(); }
    }
    
    public void Movement()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
    }

    public void Combat()
    {
        if (Input.GetMouseButtonDown(0)){ weapon.PrimaryAttack(this.transform); }
        else if(Input.GetMouseButtonDown(1)){ weapon.SecondaryAttack(this.transform); }
    }

    public void ToggleInventory(){ 
        foreach(GameObject ui in inventory){
            ui.SetActive(!ui.activeSelf);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Direction());
    }

    Vector2 Direction()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        return direction;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Item")) {
            inventoryManager.AddItem(other.GetComponent<ItemInfo>().item);
            Destroy(other.gameObject);
        }
    }
}
