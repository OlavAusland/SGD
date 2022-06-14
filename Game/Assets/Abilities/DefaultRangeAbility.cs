using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Mage/Default")]
public class DefaultRangeAbility : Ability {
    [Header("Custom")]
    public Projectile projectile;
    public float instances;

    public override void Activate(Transform caller) {
        for(int i = 0; i < instances; i++){
            GameObject obj = Resources.Load<GameObject>("Projectile");
            obj.GetComponent<ProjectileManager>().projectile = this.projectile;
            GameObject GO = Instantiate(obj, caller.position, Quaternion.identity);
        }
    }

    private Vector2 Direction(Transform caller)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)caller.position).normalized;
        return direction;
    }
}