    using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClassType
{
    Any,
    Mage,
    Bruiser,
    Ranger
}

interface IAttack
{
    public virtual void PrimaryAttack(Transform player) { }
    public virtual void SecondaryAttack(Transform player) { }
}


[System.Serializable]
public class AbilityDict {
    public string name;
    public KeyCode key;
    public Ability ability;
}

[CreateAssetMenu(menuName = "Weapon/Default")]
public class Weapon : Item
{
    public int rotationOffset;
    public ClassType classType;

    public List<AbilityDict> abilities;
    public Material material;
}