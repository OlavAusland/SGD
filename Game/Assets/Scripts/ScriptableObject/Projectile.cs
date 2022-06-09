using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName="Default Projectile", menuName="Projectiles/Default")]
public class Projectile : ScriptableObject
{
    public Sprite sprite;
    public float speed;
    public float decayTime;
    public float rotation = -90;

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

    private Vector2 Direction(Transform caller)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)caller.position).normalized;
        return direction;
    }
}
