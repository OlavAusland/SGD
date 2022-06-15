using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb { get { return GetComponent<Rigidbody2D>(); } }
    PlayerCombat pc { get { return GetComponent<PlayerCombat>(); } }
    SpriteRenderer sr { get{ return GetComponent<SpriteRenderer>(); } }


    [Header("Flip Settings")]
    public AnimationCurve flipCurve;
    public float timeScale = 2;
    private bool isFacingRight = true;

    [Header("Movement Settings")]
    public AnimationCurve moveCurve;
    public float speed = 2f;
    private float time;

    [Header("Roll Settings")]
    public float rollForce = 3;
    public float rollTime = 0.5f;
    private bool isRolling = false;


    public void Update(){
        if(!isRolling){FlipSprite();}
        if(rb.velocity.magnitude > 0)
            if(Input.GetKeyDown(KeyCode.Space) && !isRolling){StartCoroutine(Roll());}
    }

    private void FixedUpdate(){ Movement(); }

    private void FlipSprite(){
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(mouse.x > transform.position.x && !isFacingRight)
        {
            isFacingRight = true;
            StartCoroutine(AnimateFlip());
        }
        else if(mouse.x < transform.position.x && isFacingRight)
        {
            isFacingRight = false;StartCoroutine(AnimateFlip());
        }
    }

    // Move The Player Using Physics
    public void Movement()
    {
        if(isRolling){return;}
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);

        if(rb.velocity.magnitude > 0)
            AnimateMove();
        else if(time > 0)
            StartCoroutine(Animate());
    }
    
    // Animate Flip
    public IEnumerator AnimateFlip()
    {

        float time = flipCurve[flipCurve.length-1].time;
        sr.flipX = !sr.flipX;
        pc.sr.flipY = sr.flipX;
        while(time > 0)
        {
            transform.localScale = new Vector3(flipCurve.Evaluate(time), 1, 1);
            time -= Time.deltaTime * timeScale;
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = new Vector3(1, 1, 1);
        yield return null;
    }

    public void AnimateMove()
    {
        if(time <= 0){time = moveCurve[moveCurve.length - 1].time;}

        transform.localScale = new Vector3(1, moveCurve.Evaluate(time), 1);
        time -= Time.deltaTime;
    }

    private IEnumerator Animate(){
        while(time > 0){
            transform.localScale = new Vector3(1, moveCurve.Evaluate(time), 1);
            time -= Time.deltaTime / 2;
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = new Vector3(1, 1, 1);
        yield return null;
    }

    private IEnumerator Roll() {
        if(rb.velocity.magnitude <= 0){yield return null;}
        isRolling = true;
        float time = rollTime;
        rb.AddForce(rb.velocity * rollForce, ForceMode2D.Impulse);
        while(time > 0)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 360 * time / rollTime * (isFacingRight ? 1 : -1));
            time -= Time.deltaTime * rollTime;
            yield return new WaitForEndOfFrame();
        }
        transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 0);
        isRolling = false;
        yield return null;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, rb.velocity.normalized);
    }
}
