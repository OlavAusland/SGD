using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hit Effect", menuName = "Hit Effects/Slow")]
public class HitEffectSlow : HitEffect
{ 
    [Range(0, 1)]
    public float slow = 0.9f;
    public float length = 5f;

    public override void Activate(Transform other)
    {
        other.GetComponent<EnemyController>().StartCoroutine(Slow(other));
    }

    private IEnumerator Slow(Transform other)
    {
        GameObject GO = Instantiate(Resources.Load<GameObject>("Particles/Ice"), other.position, other.transform.rotation, other.transform);
        EnemyController ec = other.GetComponent<EnemyController>();

        float time = length;
        float speed = ec.enemy.info.speed;
        ec.info.speed = speed * slow;
        yield return new WaitForSeconds(length);
        Destroy(GO.transform.gameObject);
        ec.info.speed = speed;
        yield return null;
    }

}
