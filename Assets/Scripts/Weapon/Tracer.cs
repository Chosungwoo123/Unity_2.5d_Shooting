using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracer : MonoBehaviour
{
    [SerializeField] private AnimationCurve widthCurve;

    private LineRenderer line;

    public void StartWidthAnimation(float time)
    {
        line = GetComponent<LineRenderer>();

        StartCoroutine(WidthRoutine(time));
    }

    private IEnumerator WidthRoutine(float time)
    {
        float amount = 0;

        while (amount <= 1)
        {
            line.startWidth = widthCurve.Evaluate(amount);

            amount += Time.deltaTime / time;

            yield return null;
        }
        
        Destroy(gameObject);
    }
}
