using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Mage/Summon")]
public class SummonAbility : Ability
{
    public float range;
    public GameObject summon;

    public override void Activate(Transform caller)
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 spawn = mouse;

        if(Vector2.Distance(caller.position, mouse) > range)
            spawn = GetClosestPointOnCircle(caller);
        GameObject summonInstance = Instantiate(summon, spawn, caller.rotation);
        Instantiate(Resources.Load<GameObject>("Particles/Poof"), spawn, Quaternion.identity);
    }

    private Vector2 GetClosestPointOnCircle(Transform caller)
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouse - (Vector2)caller.transform.position).normalized;
        return (Vector2)caller.transform.position + direction * range;
    }
}
