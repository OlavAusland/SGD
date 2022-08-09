
using System;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPreview : MonoBehaviour
{
    RectTransform rect { get { return GetComponent<RectTransform>(); } }
    public Item item;
    public Image image;
    public TextMeshProUGUI name;
    public TextMeshProUGUI description;

    public void Start() {UpdateItem();}

    public void UpdateItem() {
        if (item != null) {
            name.text = item.name;
            image.sprite = item.icon;
            description.text = item.description;
        }
    }

    public void Update() {
        Vector2 mouse = new Vector2(Input.mousePosition.x + rect.rect.width, Input.mousePosition.y + rect.rect.height);
        transform.position = mouse;
    }
}
