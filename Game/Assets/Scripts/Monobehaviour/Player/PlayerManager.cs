using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
public class PlayerManager : MonoBehaviour
{
    public Weapon weapon;
    public List<GameObject> inventory;
    public InventoryManager inventoryManager;
    public PlayerCombat playerCombat { get {return GetComponent<PlayerCombat>();} }
    

    public float speed = 12f;
    Rigidbody2D rb { get { return GetComponent<Rigidbody2D>(); } }
    
    public void Update()
    {
        if(weapon){Combat();}
        if (Input.GetKeyDown(KeyCode.Tab)) { ToggleInventory(); }
    }

    public void FixedUpdate(){Movement();}
    
    public void Movement()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
    }

    public void Combat()
    {
        if(weapon.ability.timer <= 0){
            if (Input.GetMouseButtonDown(0)){ weapon.PrimaryAttack(this.transform); StartCoroutine(Cooldown());}
            else if(Input.GetMouseButtonDown(1)){ weapon.SecondaryAttack(this.transform); }
        }
    }

    public IEnumerator Cooldown() {
        var startTime = Time.time;

        while (weapon.ability.timer > 0)
        {
            weapon.ability.timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
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
            if(inventoryManager.AddItem(other.GetComponent<ItemInfo>().item)){
                Destroy(other.gameObject);
            }
        }
    }
}
