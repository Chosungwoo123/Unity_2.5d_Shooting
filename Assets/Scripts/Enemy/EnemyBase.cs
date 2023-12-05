using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float maxHealth;

    public GameObject dieEffect;

    private float curHealth;


    private void Start()
    {
        curHealth = maxHealth;
    }

    public void OnDamage(float damage)
    {
        curHealth -= damage;
        
        Debug.Log(curHealth);

        if (curHealth <= 0)
        {
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}