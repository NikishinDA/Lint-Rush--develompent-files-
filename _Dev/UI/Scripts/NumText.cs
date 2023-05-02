using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumText : MonoBehaviour
{
    [SerializeField] private AnimationCurve flyCurve;
    [SerializeField] private float animTime;

    private void Start()
    {
        StartCoroutine(FlyAnim(animTime));
    }

    private IEnumerator FlyAnim(float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            Vector3 pos = transform.localPosition;
            pos.y += flyCurve.Evaluate(t);
            transform.localPosition = pos;
            yield return null;
        }
        
        Destroy(gameObject);
    }
}