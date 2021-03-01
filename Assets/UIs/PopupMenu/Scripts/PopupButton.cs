using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupButton : MonoBehaviour
{
    public AnimationCurve ac;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Animate(float start, float end, float angle, float time, float waittime = 0f)
    {
        yield return new WaitForSeconds(waittime);
        float myTime = 0f;
        while (myTime < time)
        {
            myTime += Time.deltaTime;
            float x = ac.Evaluate(myTime / time);
            Vector3 startPos = Publics.CirclePos(start, angle);
            Vector3 endPos = Publics.CirclePos(end, angle);
            transform.localPosition = Vector3.Lerp(startPos, endPos, x);
            yield return null;
        }
        transform.localPosition = Publics.CirclePos(end, angle);
        if (end == 0f)
            gameObject.SetActive(false);
    }

}
