using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public Item item;

    public void Start(){
        if(item){
            transform.gameObject.name = $"Item: {item.name}";
        }
    }
}
