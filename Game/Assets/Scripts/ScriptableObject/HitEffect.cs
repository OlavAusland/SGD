using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hit Effect", menuName = "Hit Effects/Default")]
public class HitEffect : ScriptableObject
{
    public GameObject particle;

    public virtual void Activate(Transform other)
    {
        Instantiate(particle, other.position, other.transform.rotation, other.transform);
    }
}
