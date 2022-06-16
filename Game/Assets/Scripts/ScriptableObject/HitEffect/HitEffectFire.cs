using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hit Effect", menuName = "Hit Effects/FireDOT")]
public class HitEffectFire : HitEffect
{
    public int damage = -2;
    public float length;

    public override void Activate(Transform other)
    {
        other.GetComponent<EnemyController>().StartCoroutine(Fire(other));
    }

    public IEnumerator Fire(Transform other)
    {
        GameObject GO = Instantiate(Resources.Load<GameObject>("Particles/Fire"), other.position, Quaternion.identity, other.transform);
        EnemyController ec = other.GetComponent<EnemyController>();
        float time = length;
        while (time >= 0)
        {
            ec.TakeDamage(damage);
            time -= 1;
            yield return new WaitForSeconds(1);
        }
        Destroy(GO.transform.gameObject);
        yield return null;
    }
}
