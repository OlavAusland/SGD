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
    public virtual void ChargeAttack(Transform player) { }
    public virtual void SecondaryAttack(Transform player) { }
}


public class Weapon : Item, IAttack
{
    public int rotationOffset;
    public ClassType classType;

    public virtual void PrimaryAttack(Transform player) { MonoBehaviour.print("Primary Attack"); }
    public virtual void ChargeAttack(Transform player) { MonoBehaviour.print("Charge Attack"); }
    public virtual void SecondaryAttack(Transform player) { MonoBehaviour.print("Secondary Attack"); }
}