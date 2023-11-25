using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    private SpriteRenderer sr;

    public void InitAfterImage(Sprite sprite, Vector3 scale)
    {
        sr = GetComponent<SpriteRenderer>();

        sr.sprite = sprite;
        transform.localScale = scale;
    }
    
    private void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
}