using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeThrow", menuName = "Abilities/Melee/MeleeThrow")]
public class MeleeThrow : Ability
{
    public AnimationCurve curve;
    [Range(0, 1)]
    public float speed = 0.5f;
    public float range = 10f;

    public override void Activate(Transform caller){
        PlayerCombat pc = caller.GetComponent<PlayerCombat>();
        pc.StartCoroutine(Throw(caller));
    }

    public IEnumerator Throw(Transform caller)
    {
        Transform weapon = caller.GetComponent<PlayerCombat>().weaponTransform;

        Vector3 startPos = weapon.position;
        Vector3 endPos =  (Vector2)startPos + Direction(caller) * range;

        float start = Time.time;

        while (Time.time < start + speed)
        {
            float completion = (Time.time - start) / speed;
            weapon.eulerAngles = new Vector3(0, 0, 4 * 360 * curve.Evaluate(completion));
            weapon.position = Vector3.Lerp(weapon.transform.parent.position + new Vector3(0, -0.13f, 0), endPos, curve.Evaluate(completion));
            yield return null;
        }
    }
}
