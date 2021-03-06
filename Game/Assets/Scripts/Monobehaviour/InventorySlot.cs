using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public Image image { get { if (transform.childCount > 0) { return transform.GetChild(0).GetComponent<Image>(); } else { return null; } } }
    public ItemType canHold;


    public void Start() { UpdateSlot(); }
    public void UpdateSlot(){
        if (item) { 
            if(image){
                image.sprite = item.icon; 
            }else{
                GameObject itemObject = new GameObject("Item");
                itemObject.tag = "InventoryItem";
                itemObject.transform.SetParent(transform);
                itemObject.AddComponent<Image>().sprite = item.icon;
                itemObject.GetComponent<RectTransform>().sizeDelta = new Vector2(75, 75);
                itemObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
            }
        }
    }
}
