using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hit Effect", menuName = "Hit Effects/Heal")]
public class HitEffectHeal : HitEffect
{
    public int value;
    public float length = 5;

    public override void Activate(Transform other)
    {
        other.GetComponent<EnemyController>().StartCoroutine(Heal(other));
    }

    public IEnumerator Heal(Transform other)
    {
        GameObject GO = Instantiate(Resources.Load<GameObject>("Particles/Healing"), other.position, Quaternion.identity, other.transform);
        EnemyController ec = other.GetComponent<EnemyController>();
        float time = length;
        while(time >= 0)
        {
            ec.GiveHealth(value);
            time -= 1;
            yield return new WaitForSeconds(1);
        }
        Destroy(GO.transform.gameObject);
        yield return null;
    }
}
