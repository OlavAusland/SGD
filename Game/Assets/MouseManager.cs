using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Mouse Sprites")]
    public Texture2D _default;
    public Texture2D _select;

    private void Start(){Cursor.SetCursor(_default, Vector2.zero, CursorMode.Auto);}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerEnter)
        {
            print(eventData.pointerEnter.name);
            if(eventData.pointerEnter.transform.CompareTag("InventoryItem"))
            {
                Cursor.SetCursor(_select, Vector2.zero, CursorMode.Auto);
            }   
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
