    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDOT : MonoBehaviour
{
    EnemyController ec { get{ return transform.parent.GetComponent<EnemyController>(); } }
    public int damage = 10;
    public float duration = 5;


    public void Start(){StartCoroutine(DoDamage());}


    private IEnumerator DoDamage()
    {
        while(duration > 0)
        {
            ec.info.health -= damage;
            yield return new WaitForSeconds(1);
            duration -= 1;
        }
        Destroy(this.gameObject);
    }
}
