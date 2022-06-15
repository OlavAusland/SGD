using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingNumber : MonoBehaviour
{
    public float value { set { text.text = value.ToString(); value = value;} }
    public float speed = 1;
    public AnimationCurve curve;
    public Color color;

    TextMeshProUGUI text { get{ return GetComponent<TextMeshProUGUI>(); } }
    RectTransform rect { get { return GetComponent<RectTransform>(); } }

    private void Start(){StartCoroutine(Animate());}

    public IEnumerator Animate()
    {
        text.color = color;
        float time = curve[curve.length - 1].time;
        while(time > 0){
            text.color = Color.Lerp(text.color, new Color(text.color.r, text.color.g, text.color.b, 0), Time.deltaTime / time);
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, rect.anchoredPosition + new Vector2(0, (curve.Evaluate(curve[curve.length - 1].time - time) * speed)), Time.deltaTime);
            time -= Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);
        yield return null;
    }
    
}
