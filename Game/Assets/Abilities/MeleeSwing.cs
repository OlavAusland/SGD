using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Melee Swing", menuName="Abilities/Melee/Swing")]
public class MeleeSwing : Ability
{
    public AnimationCurve curve;
    [Range(0, 1)]
    public float speed = 0.5f;

    public override void Activate(Transform caller){
        PlayerCombat pc = caller.GetComponent<PlayerCombat>();
        pc.StartCoroutine(Swing(caller));
    }

    public IEnumerator Swing(Transform caller)
    {
        PlayerCombat pc = caller.GetComponent<PlayerCombat>();
        Transform weapon = pc.weaponTransform;

        Vector3 startRot = weapon.eulerAngles;
        float start = Time.time;

        while(Time.time < start + speed)
        {
            float completion = (Time.time - start) / speed;
            float dir = (weapon.GetComponent<SpriteRenderer>().flipY ? 1 : -1);
            weapon.eulerAngles = new Vector3(0, 0, startRot.z + (360 * curve.Evaluate(completion) * dir));
            yield return null;
        }
    }

}
