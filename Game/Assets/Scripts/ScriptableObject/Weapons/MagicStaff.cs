using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Mage/Staff")]
public class MagicStaff : Weapon
{
    public GameObject projectile;

    public override void PrimaryAttack(Transform player)
    {
        GameObject GO = Instantiate(projectile, player.position, Quaternion.identity);
        GO.transform.right = Direction(player);
    }

    Vector2 Direction(Transform center)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)center.position).normalized;
        return direction;
    }

}