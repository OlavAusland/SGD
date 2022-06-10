using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerCombat : MonoBehaviour
{
    PlayerManager pm { get { return GetComponent<PlayerManager>(); } }
    public Transform weaponTransform;

    public void Update(){if(pm.weapon){Rotate();}}

    void FaceMouse(){ weaponTransform.right = Direction();}

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
