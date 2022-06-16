using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName="Default Projectile", menuName="Projectiles/Default")]
public class Projectile : ScriptableObject
{
    public Sprite sprite;
    public int damage;
    public float speed;
    public float decayTime;
    public float rotation = -90;

    public List<HitEffect> hitEffects;

    public virtual IEnumerator Decay(Transform caller)
    {
        yield return new WaitForSeconds(decayTime);
        Destroy(caller.gameObject);

    }

    public virtual void Move(Rigidbody2D rb2D, Transform caller)
    {
        rb2D.AddForce(Direction(caller) * speed, ForceMode2D.Impulse);
        Vector2 dir = caller.GetComponent<Rigidbody2D>().velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        caller.rotation = Quaternion.AngleAxis(angle + rotation, Vector3.forward);
    }

    public virtual void OnHit(Transform other)
    {
        other.GetComponent<EnemyController>().TakeDamage(damage);
        foreach(HitEffect effect in hitEffects)
        {
            effect.Activate(other);
        }
        //GameObject Fire = Instantiate(Resources.Load("Particles/Fire"), other.transform.position, Quaternion.identity) as GameObject;
        //Fire.transform.parent = other.transform;
    }

    private Vector2 Direction(Transform caller)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)caller.position).normalized;
        return direction;
    }
}
