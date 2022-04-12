using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Math;
using UnityEngine;

public class FadingPulse : MonoBehaviour
{

    public Vector3 StartingScale = Vector3.one;
    public Vector3 ScaleVariance = new Vector3(0.25f, 0, 0.25f);

    void Update()
    {
        
    }

    public void StartPulse()
    {
        StopAllCoroutines();
        StartCoroutine(Pulse());
    }

    private IEnumerator Pulse()
    {
        float timeElapsed = 0;
        while (timeElapsed < 60)
        {
            float modifier = -(Mathf.PI / 4f) + (1 / 2f) * Mathf.Sin(timeElapsed) + Mathf.Pow(timeElapsed, 0.9f);
            GetComponent<RectTransform>().localScale = Vector3.one + (modifier * ScaleVariance);
            yield return new WaitForEndOfFrame();
            timeElapsed += Time.deltaTime;
        }
    }


}
