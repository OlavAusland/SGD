using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : ScriptableObject
{
    public string name;
    public Sprite icon;

    public virtual void PrimaryAttack() 
    {
        Debug.Log("Fire!");
    }
}

[CreateAssetMenu(menuName = "Weapons/Guns/Submachine")]
public class SubmachineGun : Weapon
{
    public float fireRate;
    public float ammunition;

    public override void PrimaryAttack() {
        Debug.Log("Shoot!");
    }
}

public class Staff : Weapon
{
    public GameObject particles;
}

[CreateAssetMenu(menuName = "Weapons/Staff/FireStaff")]
public class FireStaff : Staff
{
    public override void PrimaryAttack()
    {
        Debug.Log("Cast Fire Magic");
    }
}
[CreateAssetMenu(menuName = "Weapons/Staff/IceStaff")]
public class IceStaff : Staff
{
    public float fireRate;

    public override void PrimaryAttack()
    {
        Debug.Log("Cast Ice Magic");
    }
}
