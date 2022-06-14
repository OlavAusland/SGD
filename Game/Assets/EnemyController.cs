using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Enemy enemy;
    public EnemyInfo info;
    public Rigidbody2D rb { get{ return GetComponent<Rigidbody2D>(); } }
    SpriteRenderer sr { get { return GetComponent<SpriteRenderer>(); } }

    int index = 0;
    public List<Transform> path;

    [Header("Movement")]
    public AnimationCurve moveCurve;
    private bool isFacingRight = true;
    public float timeScale = 1.5f;

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
        rb.velocity = Direction(transform.position, path[index].position) * info.speed * Time.deltaTime;
        if (info.health <= 0){enemy.Die(this.transform);}

        if(rb.velocity.x > 0 && !isFacingRight){
            isFacingRight = true;
            StartCoroutine(AnimateFlip());
        }else if(rb.velocity.x < 0 && isFacingRight){
            isFacingRight = false;
            StartCoroutine(AnimateFlip());
        }
    }

    public IEnumerator AnimateFlip()
    {
        float time = moveCurve[moveCurve.length-1].time;
        sr.flipX = !sr.flipX;
        while(time > 0)
        {
            transform.localScale = new Vector3(moveCurve.Evaluate(time), 1, 1);
            time -= Time.deltaTime * timeScale;
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = new Vector3(1, 1, 1);
        yield return null;
    }

    Vector3 Direction(Vector2 a, Vector2 b){ return (b - a).normalized; }
}
