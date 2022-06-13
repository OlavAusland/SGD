using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Enemy enemy;
    public EnemyInfo info;
    Rigidbody2D rb { get{ return GetComponent<Rigidbody2D>(); } }

    int index = 0;
    public List<Transform> path;

    private void Start(){LoadValues();}

    public void LoadValues()
    {
        info.health = enemy.info.health;
        info.armor = enemy.info.armor;
        info.speed = enemy.info.speed;
    }

    public void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, path[index].position) <= 0.1f){
            if(index == path.Count - 1){index = 0;}
            else{index++;}
        }
        rb.MovePosition(transform.position + Direction(transform.position, path[index].position) * info.speed * Time.deltaTime);
        if (info.health <= 0){enemy.Die(this.transform);}
    }

    Vector3 Direction(Vector2 a, Vector2 b){ return (b - a).normalized; }
}
