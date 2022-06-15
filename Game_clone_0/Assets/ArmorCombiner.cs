using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorCombiner : MonoBehaviour
{
    SpriteRenderer sr { get { return GetComponent<SpriteRenderer>(); } }
    Texture2D tex;

    public Sprite a;
    public Sprite b;

    private void Start()
    {
        tex = new Texture2D(b.texture.width, b.texture.height);
        //Combine();
    }

    public void Combine(Sprite sprite)
    {
        for(int x = 0; x < a.texture.width; x++)
        {
            for(int y = 0; y < a.texture.height; y++)
            {
                Color colorA = a.texture.GetPixel(x, y);
                Color colorB = b.texture.GetPixel(x, y);
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
