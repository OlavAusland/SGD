using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventoryManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public List<InventorySlot> slots = new List<InventorySlot>();

    public Transform item;


    private void Start() { Initialize(); }

    public void Initialize()
    {
        foreach (Transform child in transform.GetChild(0)) { slots.Add(child.GetComponent<InventorySlot>()); }
    }

    public void AddItem(Item item){
        foreach(InventorySlot slot in slots){
            if(slot.item == null){
                slot.item = item;
                slot.UpdateSlot();
                break;
            }
        }
    }

    private void Update() { if (item) { item.position = Input.mousePosition; } }

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
                    eventData.pointerEnter.transform.GetComponent<InventorySlot>().item = item.parent.GetComponent<InventorySlot>().item;
                    item.parent.GetComponent<InventorySlot>().item = null;
                    item.SetParent(eventData.pointerEnter.transform);
                }
            }
            else
            {
                DropItem();
            }
            item.transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            item.GetComponent<Image>().raycastTarget = true;
            item = null;
        }
    }

    private void DropItem()
    {
        GameObject itemObject = Resources.Load<GameObject>("ItemPouch");
        itemObject.GetComponent<ItemInfo>().item = item.parent.GetComponent<InventorySlot>().item;
        Instantiate(itemObject, Vector3.zero, Quaternion.identity);
        item.parent.GetComponent<InventorySlot>().item = null;
        Destroy(item.gameObject);
    }

    private void Swap(Transform other){}
}
