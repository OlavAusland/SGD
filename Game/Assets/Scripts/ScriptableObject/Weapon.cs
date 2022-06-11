using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClassType
{
    Mage,
    Bruiser,
    Ranger
}

interface IAttack
{
    public virtual void PrimaryAttack(Transform player) { }
    public virtual void SecondaryAttack(Transform player) { }
}


public class Weapon : Item, IAttack
{
    public int rotationOffset;
    public ClassType classType;

    [Header("Abilities")]
    public Ability primary;
    public Ability secondary;

    public virtual void PrimaryAttack(Transform player) { primary.Activate(player); }
    public virtual void SecondaryAttack(Transform player) { secondary.Activate(player); }
}