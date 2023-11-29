using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [SerializeField] private WeaponDetails weaponDetails;
    [SerializeField] private Transform shotPos;
    [SerializeField] private Camera camera;

    public LayerMask enemyLayer;
    public LayerMask shotLayer;

    public TrailRenderer tracerEffect;

    private float fireRate;
    private float fireTimer;

    private void Start()
    {
        InitWeapon(weaponDetails);
    }

    private void Update()
    {
        AttackUpdate();
    }

    private void AttackUpdate()
    {
        if (Input.GetMouseButton(0) && fireTimer >= fireRate)
        {
            ShotWeapon();
            fireTimer = 0;
        }

        fireTimer += Time.deltaTime;
    }
    
    private void ShotWeapon()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;

        Vector3 dir = Vector3.zero;

        var tracer = Instantiate(tracerEffect, shotPos.position, Quaternion.identity);
        tracer.AddPosition(shotPos.position);
        
        float angle = 0;
        
        // 마우스 위치에 적이 있으면 총알 방향을 그쪽으로 정함
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
        {
            dir = hit.collider.transform.position - shotPos.position;
            
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

        angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        
        // 플레이어 오브젝트의 스케일이 -1일때 앵글이 이상해지는 버그를 방지하기 위한 삼항연산자
        shotPos.localRotation = Quaternion.Euler(0, transform.root.localScale.x > 0 ? angle : -angle, 0);
        
        ray.origin = shotPos.position;
        ray.direction = shotPos.forward.normalized;
                
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, shotLayer))
        {
            tracer.AddPosition(hit.point);
        }
    }

    public void InitWeapon(WeaponDetails weaponDetail)
    {
        weaponDetails = weaponDetail;

        fireRate = weaponDetails.fireRate;
        fireTimer = 0;
    }
}