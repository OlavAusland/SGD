using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    EnemyController ec { get{ return transform.parent.GetComponent<EnemyController>(); } }
    public float duration = 5;
    public float slow = 0.5f;


    public void Start(){StartCoroutine(DoDamage());}


    private IEnumerator DoDamage()
    {
        while(duration > 0)
        {
            ec.rb.velocity *= slow;
            duration -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
    }
}
