using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IAttack
{
    public void PrimaryAttack();
    public void ChargeAttack();
    public void SecondaryAttack();
}

public enum ClassType
{
    Mage,
    Hunter,
    Bruiser
}

public class Weapon : ScriptableObject, IAttack
{
    public string name;
    public Sprite icon;
    readonly ClassType type;

    public virtual void PrimaryAttack() { }
    public virtual void ChargeAttack() { }
    public virtual void SecondaryAttack() { }   
}

public class Sword : Weapon
{
    [SerializeField]
    private ClassType type = ClassType.Hunter;
}

[CreateAssetMenu(menuName = "Weapons/Mage/Book")]
public class Book : Weapon
{
    [SerializeField]
    private ClassType type = ClassType.Mage;
}
    
[CreateAssetMenu(menuName = "Weapons/Hunter/Bow")]
public class Bow : Weapon
{
    public float fireRate;
    public float ammunition;

    public override void PrimaryAttack() {
        Debug.Log("Shoot!");
    }

}

[CreateAssetMenu(menuName = "Weapons/Mage/MagicStaff")]
public class MagicStaff : Weapon
{
    public override void PrimaryAttack()
    {
        Debug.Log("Cast Fire Magic");
    }
}