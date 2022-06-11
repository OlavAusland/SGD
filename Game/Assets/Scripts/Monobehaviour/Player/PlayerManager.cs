using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[RequireComponent(typeof(PlayerCombat))]
public class PlayerManager : MonoBehaviour
{
    public Weapon weapon;
    public float primaryCD;
    public float secondaryCD;

    public Image primaryAbility;
    public Image secondaryAbility;

    public List<GameObject> inventory;
    public InventoryManager inventoryManager;
    public PlayerCombat playerCombat { get {return GetComponent<PlayerCombat>();} }
    

    public float speed = 12f;
    Rigidbody2D rb { get { return GetComponent<Rigidbody2D>(); } }
    
    public void Update()
    {
        if(weapon){Combat();}
        if (Input.GetKeyDown(KeyCode.Tab)) { ToggleInventory(); }
        if(primaryCD > 0){
            primaryCD -= Time.deltaTime;
            primaryAbility.fillAmount = primaryCD / weapon.primary.cooldown;
        }
        if(secondaryCD > 0){
            secondaryCD -= Time.deltaTime;
            secondaryAbility.fillAmount = secondaryCD / weapon.secondary.cooldown;
        }
    }

    public void FixedUpdate(){Movement();}
    
    public void Movement()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
    }

    public void Combat()
    {
        if(weapon.primary)
        {
            if (Input.GetMouseButtonDown(0) && primaryCD <= 0)
            { 
                weapon.PrimaryAttack(this.transform);
                primaryCD = weapon.primary.cooldown;
                //StartCoroutine(Cooldown(primaryCD));
            }

        }
        if(weapon.secondary)
        {
            if(Input.GetMouseButtonDown(1) && secondaryCD <= 0)
            { 
                weapon.SecondaryAttack(this.transform); 
                secondaryCD = weapon.secondary.cooldown;
                //StartCoroutine(Cooldown(secondaryCD));
            }
        }
        
    }

    public IEnumerator Cooldown(float cd)
    {
        print("weapon cooldown");
        while(cd > 0)
        {
            print(cd);
            cd = cd - Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public void ToggleInventory(){ 
        foreach(GameObject ui in inventory){
            ui.SetActive(!ui.activeSelf);
        }
    }

    public void FlipSprite(){
        if(rb.velocity.x > 0){
            transform.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(rb.velocity.x < 0){
            transform.GetComponent<SpriteRenderer>().flipX = true;
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
