using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region 싱글톤
    
    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }

            return instance;
        }
    }
    
    #endregion

    #region 카메라 관련 오브젝트

    [Space(10)] [Header("카메라 관련 오브젝트")] 
    [SerializeField] private CameraShake cameraShake;

    public Camera mainCamera;

    #endregion

    #region UI 관련 오브젝트

    [Space(10)]
    [Header("UI 관련 오브젝트")]
    [SerializeField] private SkillGauge skillGaugePrefab;
    [SerializeField] private Transform skillGaugeParent;

    #endregion

    private int skillCount;

    private float skillExp;

    private List<SkillGauge> skillGaugeList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        skillGaugeList = new List<SkillGauge>();
    }

    public void CameraShake(float intensity, float time)
    {
        cameraShake.ShakeCamera(intensity, time);
    }

    public void InitSkillGauge(int skillGaugeCount)
    {
        for (int i = 0; i < skillGaugeCount; i++)
        {
            skillGaugeList.Add(Instantiate(skillGaugePrefab, skillGaugeParent));
            skillGaugeList[i].SetFillAmount(0);
        }

        skillCount = skillGaugeCount;
    }

    public void GetSkillExp(float exp)
    {
        skillExp = Mathf.Min(skillExp + exp, skillCount);

        for (int i = 0; i < skillCount; i++)
        {
            skillGaugeList[i].SetFillAmount(skillExp - i);
        }
    }
}