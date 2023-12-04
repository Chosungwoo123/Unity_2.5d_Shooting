using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDetails_", menuName = "ScriptableObject/WeaponDetails")]
public class WeaponDetails : ScriptableObject
{
    #region 기본 스탯

    [Space(10)] [Header("기본 스탯")] 
    public float damage;

    public float fireRate;

    public float spreadAmount;

    #endregion
}