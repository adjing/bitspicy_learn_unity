/*
角色AI系统，英雄和角色共用。英雄自动打，怪物行为
*/
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

    [Header("当前的攻击目标")]
    public float current_attack_distance = 0.0f;

    [Header("攻击开关")]
    public bool attack_open = false;

    private Animator animator;
    private string animator_parameter_cmd = "cmd";

    private void Start()
    {
        SetComponent();
        InitConfig();
        CheckAttackDistance();
    }

    private void Update()
    {
        CheckAttackDistance();
    }

    private void SetComponent()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void InitConfig()
    {
        attack_trigger_distance = 5;
    }

    /// <summary>
    /// 动态攻击距离
    /// </summary>
    /// <returns></returns>
    public bool CheckAttackDistance()
    {
        current_attack_distance = Vector3.Distance(transform.position, attack_target.position);
        attack_open = current_attack_distance < attack_trigger_distance;
        return attack_open;
    }

    public void LookAtTarget()
    {
        if(!attack_open)
        {
            return;
        }

        PlayIdle();
        //Debug.LogErrorFormat("{0}面朝向目标 LookAtTarget OK!attack_open={1}", Time.time, attack_open);
        transform.LookAt(attack_target, Vector3.up);
      
    }

    #region Animator
    public void PlayIdle()
    {
        if (animator == null)
        {
            SetComponent();
            return;
        }

        animator.SetInteger(animator_parameter_cmd, EnumPlayerState.idle);
    }

    public void PlayWalk()
    {
        if (animator == null)
        {
            SetComponent();
            return;
        }

        animator.SetInteger(animator_parameter_cmd, EnumPlayerState.walk);
    }

    public void PlayAttack()
    {
        if(animator == null)
        {
            SetComponent();
            return;
        }

        if(GetAttackState() == false)
        {
            animator.SetInteger(animator_parameter_cmd, EnumPlayerState.attack);
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