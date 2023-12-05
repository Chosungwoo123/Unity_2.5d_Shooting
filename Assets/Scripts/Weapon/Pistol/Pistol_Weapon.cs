using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Pistol_Weapon : WeaponBase
{
    protected override void ShotWeapon()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;

        Vector3 dir = Vector3.zero;

        var tracer = Instantiate(tracerEffect, shotPos.position, Quaternion.identity);
        tracer.SetPosition(0, shotPos.position);
        
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
            float hitDist;
            
            if (shotPosPlane.Raycast(ray, out hitDist))
            {
                dir = ray.GetPoint(hitDist) - shotPos.position;
                dir.y = 0;
            }
        }
        
        float randomSpread = Random.Range(0f, spreadAmount);
  
        int spreadToggle = Random.Range(0,2) * 2 -1;
        
        angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        angle += spreadToggle * randomSpread;
        
        // 플레이어 오브젝트의 스케일이 -1일때 앵글이 이상해지는 버그를 방지하기 위한 삼항연산자
        shotPos.localRotation = Quaternion.Euler(0, transform.root.localScale.x > 0 ? angle : -angle, 0);
        
        ray.origin = shotPos.position;
        ray.direction = shotPos.forward.normalized;
                
        // 벽 쪽에만 충돌됨
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, shotLayer))
        {
            tracer.SetPosition(1, hit.point);
            tracer.GetComponent<Tracer>().StartWidthAnimation(0.15f);
        }
        
        // 적들에게 데미지 주기
        foreach (var enemy in Physics.RaycastAll(shotPos.position, dir.normalized, Mathf.Infinity, enemyLayer))
        {
            if (enemy.transform.TryGetComponent(out EnemyBase enemyBase))
            {
                enemyBase.OnDamage(weaponDetails.damage);
            }
        }
        
        GameManager.Instance.CameraShake(weaponDetails.cameraShakeAmount, 0.1f);
    }
}