using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;
    public Enemy enemy;
    public EnemyInfo info;
    public Rigidbody2D rb { get{ return GetComponent<Rigidbody2D>(); } }
    SpriteRenderer sr { get { return GetComponent<SpriteRenderer>(); } }

    [Header("Movement")]
    public AnimationCurve moveCurve;
    private bool isFacingRight = true;
    public float timeScale = 1.5f;

    [Header("Health")]
    public Vector2 offset;
    Transform healthBar; // Prefab
    Image healthBarImage;
    Transform damage;

    [Header("AI")]
    public LayerMask layerMask;
    [SerializeField]
    private Transform target;
    private Vector3 lastKnownPosition;
    public float length;

    private void Start(){Initialize();}

    private void Initialize()
    {
        healthBar = Instantiate(Resources.Load<GameObject>("UI/Health"), transform.position, Quaternion.identity).transform;
        healthBar.SetParent(GameObject.Find("Canvas").transform);
        healthBar.localScale = new Vector3(1, 1, 1);
        healthBarImage = healthBar.transform.GetChild(0).GetComponent<Image>();

        // Load Enemy Info
        LoadValues();
    }

    public void LoadValues()
    {
        info.health = enemy.info.health;
        info.armor = enemy.info.armor;
        info.speed = enemy.info.speed;
    }

    public void FixedUpdate()
    {
        healthBar.position = transform.position + (Vector3)offset;

        if (info.health <= 0){Die();}

        if(rb.velocity.x > 0 && !isFacingRight){
            isFacingRight = true;
            StartCoroutine(AnimateFlip());
        }else if(rb.velocity.x < 0 && isFacingRight){
            isFacingRight = false;
            StartCoroutine(AnimateFlip());
        }

        Scan();
        Chase();
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

    public void TakeDamage(int damage)
    {
        if(damage == 0){return;}
        info.health -= damage;
        healthBarImage.fillAmount = ((float)info.health / (float)enemy.info.health);

        GameObject GO = Instantiate(Resources.Load<GameObject>("UI/FloatingNumber"), transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        GO.GetComponent<FloatingNumber>().value = damage;
    }

    public void GiveHealth(int value)
    {
        info.health += value;
        healthBarImage.fillAmount = ((float)info.health / (float)enemy.info.health);

        FloatingNumber fn = Instantiate(Resources.Load<GameObject>("UI/FloatingNumber"), transform.position, Quaternion.identity, GameObject.Find("Canvas").transform).GetComponent<FloatingNumber>();
        fn.color = Color.green;
        fn.value = value;
    }

    private void Die()
    {
        enemy.Die(this.transform); 
        Destroy(healthBar.gameObject);
    }

    Vector3 Direction(Vector2 a, Vector2 b){ return (b - a).normalized; }

    public void Scan() 
    {
        //circlecast
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, length, layerMask);

        foreach(Collider2D c in hits)
        {
            if(c.transform.CompareTag("Player"))
            {
                RaycastHit2D hit = Physics2D.Raycast(this.transform.position, (c.transform.position - transform.position).normalized, length, layerMask);
                if(hit.transform == null){return;}
                if(hit.transform.CompareTag("Player")){target = c.transform;return;}
            }

        }
        if(target)
        {
            lastKnownPosition = target.position;
            target = null;
        }
    }

    public void Chase() {
        if(target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * info.speed * Time.deltaTime;
        }else if(lastKnownPosition != Vector3.zero){
            Vector3 direction = (lastKnownPosition - transform.position).normalized;
            rb.velocity = direction * info.speed * Time.deltaTime;

            if(Vector3.Distance(transform.position, lastKnownPosition) <= 0.1f){
                lastKnownPosition = Vector3.zero;
                rb.velocity = Vector3.zero;
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, length);
        if(target){
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, target.position);
        }

        if(lastKnownPosition != Vector3.zero){
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, lastKnownPosition);
        }
    }
}
