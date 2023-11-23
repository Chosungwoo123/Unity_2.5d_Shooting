using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Bullet : MonoBehaviour
{
    public float moveSpeed = 5;
    public Vector3 _dir;

    private void Update()
    {
        Vector3 nextPos = _dir.normalized * moveSpeed * Time.deltaTime;
        transform.position += nextPos;
    }

    public void Init(Vector3 dir)
    {
        _dir = dir;
    }
}