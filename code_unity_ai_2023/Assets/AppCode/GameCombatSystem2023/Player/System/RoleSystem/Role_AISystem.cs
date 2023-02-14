/*
角色AI系统，英雄和角色共用。英雄自动打，怪物行为

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role_AISystem : MonoBehaviour
{
    /// <summary>
    /// int 1=英雄 2=怪物
    /// </summary>
    public string role_type;

    [Header("攻击触发配置,离敌人的距离")]
    public int attack_trigger_distance = 5;

    [Header("攻击目标")]
    public Transform attack_target;

    //[SerializeField]
    //public AnimationClip standAnimationClip;
    //[SerializeField]
    //public GameObject playerObject;
    //[SerializeField]
    //public AnimationClip runAnimationClip;
    //[SerializeField]
    //public AnimationClip attackAnimationClip;



    /// <summary>
    /// 动态距离-离主玩家的距离
    /// </summary>
    /// <returns></returns>
    public int CheckMainHeroDistance()
    {
        return 4;
    }

    public void LookAtTarget()
    {
        transform.LookAt(attack_target, Vector3.up);
        //Debug.LogError("LookAtTarget OK!");
    }
}