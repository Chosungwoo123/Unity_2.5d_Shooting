using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] protected WeaponDetails weaponDetails;
    [SerializeField] protected Transform shotPos;

    public LayerMask enemyLayer;
    public LayerMask shotLayer;

    public LineRenderer tracerEffect;

    protected float fireRate;
    protected float fireTimer;
    protected float spreadAmount;
    
    protected Camera camera;

    private Dictionary<int, string> d = new Dictionary<int, string>();

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

    protected abstract void ShotWeapon();

    public void InitWeapon(WeaponDetails weaponDetail)
    {
        weaponDetails = weaponDetail;

        fireRate = weaponDetails.fireRate;
        spreadAmount = weaponDetails.spreadAmount;
        
        fireTimer = 0;

        camera = GameManager.Instance.mainCamera;
    }
}