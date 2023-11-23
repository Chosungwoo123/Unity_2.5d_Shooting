using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float groundDist;

    public LayerMask terrainLayer;

    private bool isRun;
    
    private Rigidbody rigid;
    private SpriteRenderer sr;
    private Animator anim;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveUpdate();
        AnimationUpdate();
    }

    private void MoveUpdate()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
                
                Debug.Log("gg");
            }
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(x, 0, y);
        rigid.velocity = moveDir * moveSpeed;

        if (moveDir != Vector3.zero)
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }

        if (x != 0 && x < 0)
        {
            sr.flipX = true;
        }
        else if (x != 0 && x > 0)
        {
            sr.flipX = false;
        }
    }
    
    private void AnimationUpdate()
    {
        anim.SetBool("isRun", isRun);
    }
}