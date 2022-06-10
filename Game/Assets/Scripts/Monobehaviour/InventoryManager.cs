using System.Data;
using System.Runtime.CompilerServices;
using System.Diagnostics.Tracing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventoryManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerManager player;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public List<InventorySlot> toolbar = new List<InventorySlot>(3);

    public Transform item;
    public Item equippedItem = null;


    private void Start() { Initialize(); }

    public void Initialize()
    {
        foreach (Transform child in transform.GetChild(0)) { slots.Add(child.GetComponent<InventorySlot>()); }
        foreach (Transform child in transform.GetChild(1)) { toolbar.Add(child.GetComponent<InventorySlot>()); }
    }

    public bool AddItem(Item item){
        foreach(InventorySlot slot in slots){
            if(slot.item == null){
                slot.item = item;
                slot.UpdateSlot();
                return true;
            }
        }

        return false;
    }

    private void Update() { if (item) { item.position = Input.mousePosition; } EquipItem(); }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter)
        {
            if (eventData.pointerEnter.transform.CompareTag("InventoryItem"))
            {
                item = eventData.pointerEnter.transform;
                item.GetComponent<Image>().raycastTarget = false;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (item)
        {
            if (eventData.pointerEnter)
            {
                if (eventData.pointerEnter.transform.CompareTag("Slot"))
                {
                    InventorySlot slot = eventData.pointerEnter.transform.GetComponent<InventorySlot>();
                    if(slot.item == null){
                        if(SlotCanHoldItem(item.parent.GetComponent<InventorySlot>().item, slot)){
                            slot.item = item.parent.GetComponent<InventorySlot>().item;
                            item.parent.GetComponent<InventorySlot>().item = null;
                            item.SetParent(eventData.pointerEnter.transform);
                        }
                    }
                }else if(eventData.pointerEnter.transform.CompareTag("InventoryItem")){
                    if(true){
                        Transform firstParent = item.parent;
                        Transform secondParent = eventData.pointerEnter.transform.parent;

                        Item firstItem = firstParent.GetComponent<InventorySlot>().item;
                        Item secondItem = secondParent.GetComponent<InventorySlot>().item;

                        if(SlotCanHoldItem(firstItem, secondParent.GetComponent<InventorySlot>())){
                            if(SlotCanHoldItem(secondItem, firstParent.GetComponent<InventorySlot>())){
                                firstParent.GetComponent<InventorySlot>().item = secondItem;
                                secondParent.GetComponent<InventorySlot>().item = firstItem;
                                item.SetParent(secondParent);
                                eventData.pointerEnter.transform.SetParent(firstParent);
                                eventData.pointerEnter.transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                            }
                        }
                    }

                }
            }
            else
            {
                DropItem();
            }
            item.transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            item.GetComponent<Image>().raycastTarget = true;

            if(equippedItem == item){equippedItem = null;}
            item = null;
        }
    }

    private void DropItem()
    {
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPosition.z = 0;
        GameObject itemObject = Resources.Load<GameObject>("ItemPouch");
        itemObject.GetComponent<ItemInfo>().item = item.parent.GetComponent<InventorySlot>().item;
        Instantiate(itemObject, spawnPosition, Quaternion.identity);
        item.parent.GetComponent<InventorySlot>().item = null;
        Destroy(item.gameObject);
    }

    public bool SlotCanHoldItem(Item item, InventorySlot slot){
        if(item){
            if(item.itemType == slot.canHold || slot.canHold == ItemType.Any)
                return true;
        }
        return false;

    }
    private void Swap(Transform other){
        
    }

    private void EquipItem(){
        for(int i = 1; i < toolbar.Count + 1; i++)
        {
            if(Input.GetKeyDown((KeyCode)(48+i))){
                if(toolbar[i-1].item != null){
                    equippedItem = toolbar[i-1].item;
                    if(equippedItem.itemType == ItemType.Weapon){
                        player.weapon = equippedItem as Weapon;
                    }
                    else if(equippedItem.itemType == ItemType.Consumable){}
                }
                else{
                    equippedItem = null;
                    player.weapon = null;
                }
            }
        }
    }
}
