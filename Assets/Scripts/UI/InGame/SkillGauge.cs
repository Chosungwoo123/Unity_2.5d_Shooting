using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillGauge : MonoBehaviour
{
    public Image gaugeImage;
    
    public void SetFillAmount(float amount)
    {
        gaugeImage.fillAmount = amount;
    }
}