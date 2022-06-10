using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoInstance : MonoBehaviour
{
    public static MonoInstance instance;

    private void Start()
    {
        MonoInstance.instance = this;
    }
}
public enum AbilityType {
    Melee,
    Ranged
}

public class Ability : ScriptableObject
{
    public string _name;
    public Sprite icon;
    public AbilityType type;
    public float cooldown;
    public float timer;


    public virtual void Activate(Transform caller){
        MonoBehaviour.print("Attack");
        timer = cooldown;
    }
}