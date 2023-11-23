using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Weapon : MonoBehaviour
{
    public Transform shotPos;
    public Camera camera;
    public Test_Bullet bullet;
    public LayerMask enemyLayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shot();
        }
    }
    
    private void Shot()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        Vector3 dir = Vector3.zero;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
        {
            dir = hit.point - shotPos.position;

            dir = new Vector3(dir.x, 0, dir.z);
        }
        
        var _bullet = Instantiate(bullet, shotPos.position, Quaternion.identity);
        
        _bullet.Init(dir);
    }
}