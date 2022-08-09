using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hit Effects", menuName = "Hit Effects/Stun")]
public class HitEffectStun : HitEffect
{
    public float length = 2f;
    
    public override void Activate(Transform other)
    {
        other.GetComponent<EnemyController>().StartCoroutine(Stun(other));
    }

    public IEnumerator Stun(Transform other)
    {
        GameObject GO = new GameObject();
        if(!HasEffect(other, "Stun(Clone)"))
            GO = Instantiate(particle, other.position, other.transform.rotation, other.transform);
        yield return new WaitForSeconds(length);
        Destroy(GO.transform.gameObject);
    }
}
