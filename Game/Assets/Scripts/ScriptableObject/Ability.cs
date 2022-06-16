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
    public KeyCode key;
    public float cooldown;

    public virtual void Activate(Transform caller){}

    protected Vector2 Direction(Transform center)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)center.position).normalized;
        return direction;
    }
}