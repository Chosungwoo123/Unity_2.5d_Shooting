using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region 기본 스탯
    
    [Space(10)]
    [Header("기본 스탯")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float groundDist;

    #endregion

    #region 대쉬 관련 스탯

    [Space(10)]
    [Header("대쉬 관련 스탯")]
    [SerializeField] private float dashTime;
    [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;
    [SerializeField] private float dashCoolTime;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float afterImageDistance;

    #endregion

    #region 프리팹 관련 오브젝트

    [Space(10)] [Header("프리팹 관련 오브젝트")] 
    [SerializeField] private AfterImage afterImagePrefab;

    #endregion
    
    public LayerMask terrainLayer;

    private float dashTimer;
    
    private bool isRun;
    private bool isDash;
    private bool canDash;

    private Vector3 moveDirection;

    private WaitForSeconds dashTimeSeconds;
    
    private Rigidbody rigid;
    private SpriteRenderer sr;
    private Animator anim;

    private void Start()
    {
        // 변수 초기화
        rigid = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        canDash = true;

        dashTimeSeconds = new WaitForSeconds(dashTime);
    }

    private void Update()
    {
        InputUpdate();
        DashUpdate();
        AnimationUpdate();
    }

    private void FixedUpdate()
    {
        MoveUpdate();
    }

    private void InputUpdate()
    {
        // Dash
        if (Input.GetKeyDown(dashKey) && canDash)
        {
            StartCoroutine(DashRoutine());
        }
    }
    
    private void DashUpdate()
    {
        if (isDash)
        {
            return;
        }
        
        dashTimer -= Time.deltaTime;

        if (dashTimer <= 0)
        {
            canDash = true;
        }
    }

    private void MoveUpdate()
    {
        if (isDash)
        {
            return;
        }
        
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
            }
        }

        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.z = Input.GetAxisRaw("Vertical");
        
        rigid.velocity = moveDirection.normalized * moveSpeed;

        if (moveDirection.normalized != Vector3.zero)
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }

        if (moveDirection.x != 0 && moveDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDirection.x != 0 && moveDirection.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    
    private IEnumerator DashRoutine()
    {
        float timer = 0f;
        
        isDash = true;
        canDash = false;
        
        if (moveDirection == Vector3.zero)
        {
            moveDirection.z = 1;
        }
        
        Vector3 lastAfterImagePos = transform.position;

        Instantiate(afterImagePrefab, lastAfterImagePos, Quaternion.identity).InitAfterImage(sr.sprite, transform.localScale);
        
        rigid.velocity = moveDirection.normalized * dashSpeed;
        
        while (timer <= dashTime)
        {
            if (Vector3.Distance(transform.position, lastAfterImagePos) >= afterImageDistance)
            {
                lastAfterImagePos = transform.position;
                
                var afterImage = Instantiate(afterImagePrefab, lastAfterImagePos, Quaternion.identity);
                afterImage.InitAfterImage(sr.sprite, transform.localScale);
            }

            timer += Time.deltaTime;

            yield return null;
        }
        
        isDash = false;
        
        dashTimer = dashCoolTime;
        
        rigid.velocity = Vector3.zero;
    }
    
    private void AnimationUpdate()
    {
        anim.SetBool("isRun", isRun);
    }
}