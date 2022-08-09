using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    Texture2D tex;
    Sprite baseSprite { get { return Resources.Load<Sprite>("Player"); } }
    SpriteRenderer sr { get { return GetComponent<SpriteRenderer>(); } }
    public List<InventorySlot> equipment;
    private void Start(){tex = new Texture2D(sr.sprite.texture.width, sr.sprite.texture.height);}
    public void Update(){if(Input.GetKeyDown(KeyCode.Tab)){Combine();}}

    public void Combine()
    {
        sr.sprite = baseSprite;
        foreach(InventorySlot slot in equipment)
        {
            if(slot.item == null){continue;}
            for(int x = 0; x < slot.item.icon.texture.width; x++)
            {
                for(int y = 0; y < slot.item.icon.texture.height; y++)
                {
                    Color colorA = slot.item.icon.texture.GetPixel(x, y);
                    Color colorB = sr.sprite.texture.GetPixel(x, y);
                    //check if color is transparent
                    if(colorA.a != 0)
                    {
                        tex.SetPixel(x, y, colorA);
                    }else if(colorB.a != 0){
                        tex.SetPixel(x, y, colorB);
                    }else{
                        tex.SetPixel(x, y, Color.clear);
                    }
                }
            }
            tex.filterMode = FilterMode.Point;
            tex.Apply();
            sr.sprite = Sprite.Create(tex, new Rect(0,0,tex.width,tex.height), new Vector2(0.5f, 0.5f), 35);
        }
    }
}
