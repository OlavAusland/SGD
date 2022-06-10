using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Potion", menuName="Consumable/Potion")]
public class Consumable : Item
{
    public virtual void Consume(Transform player) { }
}
