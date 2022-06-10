using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public static PlayerCombat instance;

    SpriteRenderer sr { get { return transform.GetChild(0).GetComponent<SpriteRenderer>(); } }
    PlayerManager pm { get { return GetComponent<PlayerManager>(); } }
    public Transform weaponTransform;


    public void Update(){if(pm.weapon){Rotate();}}

    public void SetWeapon(Sprite sprite){sr.sprite = sprite;}

    Vector2 Direction()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        return direction;
    }

    void Rotate(){
        Vector2 dir = Direction();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weaponTransform.rotation = Quaternion.AngleAxis(angle + pm.weapon.rotationOffset, Vector3.forward);
    }
}
