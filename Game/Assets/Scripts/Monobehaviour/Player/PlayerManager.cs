using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(PlayerCombat), typeof(SpriteRenderer))]
public class PlayerManager : MonoBehaviour
{
    public PlayerMovement pm { get {return GetComponent<PlayerMovement>(); } }
    public PlayerEquipment pe { get { return GetComponent<PlayerEquipment>(); } }

    public List<GameObject> inventory;
    public InventoryManager inventoryManager;
    SpriteRenderer sr { get { return GetComponent<SpriteRenderer>(); } }

    [Header("Health Data")]
    [SerializeField] private int _health;
    public int health 
    { 
        get {
            print($"Player Health: {_health}"); 
            return _health;
        }
        set {
            print($"Health: {_health} - Value: {value}");
            _health = value;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) { ToggleInventory(); }
        else if(Input.GetKeyDown(KeyCode.Equals)){GiveHealth(1);}
        else if(Input.GetKeyDown(KeyCode.Minus)){TakeDamage(10);}
    }

    public void ToggleInventory(){ 
        foreach(GameObject ui in inventory){
            //StartCoroutine(AnimateHideUI(ui));
            ui.SetActive(!ui.activeSelf);
        }
    }

    public IEnumerator AnimateHideUI(GameObject obj)
    {
        RectTransform rt = obj.GetComponent<RectTransform>();
        float time = 1;
        while (time >= 0)
        {
            rt.localPosition = new Vector2(rt.localPosition.x, rt.localPosition.y);
            time -= Time.deltaTime;
            yield return null;
        }
        obj.SetActive(!obj.activeSelf);
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Item")) {
            if(inventoryManager.AddItem(other.GetComponent<ItemInfo>().item)){
                Destroy(other.gameObject);
            }
        }
    }

    private void GiveHealth(int value){
        health += value;
        FloatingNumber number = Instantiate(Resources.Load<GameObject>("UI/FloatingNumber"), transform.position, Quaternion.identity, GameObject.Find("Canvas").transform).GetComponent<FloatingNumber>();
        number.value = value;
        number.color = Color.green;
    }

    private void TakeDamage(int value){
        health -= value;
        FloatingNumber number = Instantiate(Resources.Load<GameObject>("UI/FloatingNumber"), transform.position, Quaternion.identity, GameObject.Find("Canvas").transform).GetComponent<FloatingNumber>();
        number.value = value;
        number.color = Color.red;
    }
}
