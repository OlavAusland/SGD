using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    Rigidbody2D rb { get { return GetComponent<Rigidbody2D>(); } }
    SpriteRenderer sr { get { return GetComponent<SpriteRenderer>(); } }
    public Projectile projectile;

    private void Start()
    {
        sr.sprite = projectile.sprite;
        StartCoroutine(projectile.Decay(this.transform));
        projectile.Move(rb, this.transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.CompareTag("Enemy")){
            projectile.OnHit(other.transform);
            Destroy(transform.gameObject);
        }
    }
}
