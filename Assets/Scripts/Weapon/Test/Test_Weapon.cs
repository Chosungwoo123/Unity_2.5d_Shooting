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
        
        // 마우스 위치에 적이 있으면 총알 방향을 그쪽으로 정함
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
        {
            dir = hit.point - shotPos.position;
       
            dir = new Vector3(dir.x, 0, dir.z);
        }

        // 마우스 위치에 적이 없으면 마우스 위치로 방향을 정함
        if (dir == Vector3.zero)
        {
            Plane shotPosPlane = new Plane(Vector3.up, shotPos.position);
            float hitDist = 0.0f;
            
            if (shotPosPlane.Raycast(ray, out hitDist))
            {
                dir = ray.GetPoint(hitDist) - shotPos.position;
                dir.y = 0;
            }
        }
        
        var _bullet = Instantiate(bullet, shotPos.position, Quaternion.identity);
        
        _bullet.Init(dir);
    }
}