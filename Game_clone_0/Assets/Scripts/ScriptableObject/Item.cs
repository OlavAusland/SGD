using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType {
    Weapon,
    Consumable,
    Armor,
    Any
}
public class Item : ScriptableObject
{
    public string name;
    public Sprite icon;
    public ItemType itemType;
}
