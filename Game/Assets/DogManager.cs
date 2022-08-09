using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviourType {
    Idle,
    Follow,
    Wander,
    Chase,
    Flee,
    Attack,
    Eat,
    Sleep,
    Dead
}

[System.Serializable]
public class DogBehaviour {
    public string name;
    public BehaviourType type;
    [Range(0, 100)]
    public int chance;
    public int length;
}

public class DogManager : MonoBehaviour
{
    Animator anim { get { return GetComponent<Animator>(); } }
    Rigidbody2D rb { get { return GetComponent<Rigidbody2D>(); } }
    SpriteRenderer sr { get { return GetComponent<SpriteRenderer>(); } }

    public Transform owner;
    public Vector3 target;
    public BehaviourType current;
    public List<DogBehaviour> behaviours;

    public float counter = 0;
    public float speed = 4f;

    public void FixedUpdate()
    {
        if(counter > 0){counter -= Time.deltaTime;return;}
        else{Follow();}
        FlipSprite();
    
    }

    public void FlipSprite(){
        if(rb.velocity.x < 0){
            sr.flipX = true;
        } else {sr.flipX = false;}                                                                                                                              
    }

    public void PickBehaviour()
    {
        foreach(DogBehaviour behaviour in behaviours) {
            if(Random.Range(0, 100) >= behaviour.chance) {
                current = behaviour.type;
                counter = behaviour.length;
                return;
            }
        }
        return;
    }

    public void Follow() {
        if(owner)
        {
            target = owner.position;
            anim.SetInteger("Action", (int)current);
            if(Vector3.Distance(transform.position, target) <= 0.4f){
                rb.velocity = Vector3.zero;
                current = BehaviourType.Idle;
                return;
            }else{current = BehaviourType.Follow;}
            Vector3 direction = (target - transform.position).normalized;
            rb.velocity = direction * speed * Time.deltaTime;
        }else{target = Vector3.zero;}
    }

    public void Wander() {
        if(target == Vector3.zero){
            target = owner.position + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
        }
        Vector3 direction = (target - transform.position).normalized;
        rb.velocity = direction * speed * Time.deltaTime;

    }
}
