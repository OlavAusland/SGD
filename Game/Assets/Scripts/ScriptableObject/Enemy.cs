using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IEnemy
{
    public virtual void Move(Transform caller){}
    public virtual void Attack(Transform caller){}
    public virtual void Die(Transform caller){}
}

[System.Serializable]
public class EnemyInfo 
{
    public int health = 100;
    public float armor = 0;
    public float speed = 1;
}

public class Enemy : ScriptableObject, IEnemy
{
    public EnemyInfo info;

    public virtual void Move(Transform caller)
    {
        Debug.Log("Enemy moves");
    }

    public virtual void Attack(Transform caller)
    {
        Debug.Log("Enemy attacks");
    }

    public virtual void Die(Transform caller)
    {
        Debug.Log("Enemy dies");
        MonoBehaviour.Destroy(caller.gameObject);
    }
}

