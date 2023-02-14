/*
角色AI系统，英雄和角色共用。英雄自动打，怪物行为

*/
using System;
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

    private Animator animator;

    //[SerializeField]
    //public AnimationClip standAnimationClip;
    //[SerializeField]
    //public GameObject playerObject;
    //[SerializeField]
    //public AnimationClip runAnimationClip;
    //[SerializeField]
    //public AnimationClip attackAnimationClip;

    private void Start()
    {
        SetComponent();
        InitConfig();
    }

    private void SetComponent()
    {
        animator = GetComponent<Animator>();
    }

    private void InitConfig()
    {
        attack_trigger_distance = 5;
    }

    /// <summary>
    /// 动态攻击距离
    /// </summary>
    /// <returns></returns>
    public int CheckAttackDistance()
    {
        return 4;
    }

    public void LookAtTarget()
    {
        transform.LookAt(attack_target, Vector3.up);
        PlayAttack();
        //Debug.LogError("面朝向目标 LookAtTarget OK!");
    }

    #region Animator
    private void PlayAttack()
    {
        if(animator == null)
        {
            SetComponent();
            return;
        }

        if(GetAttackState() == false)
        {
            animator.SetTrigger("trigger_attack_1");
        }
    }

    private bool GetAttackState()
    {
        var b = false;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack_1"))
        {
            b = true;
            //Debug.LogError("attack_1");
        }

        return b;
    }

    #endregion
}