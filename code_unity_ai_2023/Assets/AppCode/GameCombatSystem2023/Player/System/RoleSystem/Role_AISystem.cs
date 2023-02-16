/*
角色AI系统，英雄和角色共用。英雄自动打，怪物行为
*/
using System;
using System.Data;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Role_AISystem : MonoBehaviour
{
    /// <summary>
    /// int 1=英雄 2=怪物
    /// </summary>
    public string role_type;

    [Header("编号GUID")]
    public string role_guid="1001";

    [Header("数据")]
    public GameRole_Data current_role_data;

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
        FillData();
        SetComponent();
        InitConfig();
        CheckAttackDistance();
    }

    private void FillData()
    {
        current_role_data = new GameRole_Data();
        var dbrow = GetDBData();
        if(dbrow != null )
        {
            current_role_data.hp = dbrow.health;
        }
        else
        {
            Debug.LogErrorFormat("{0} role_guid={1}", Time.time, role_guid);
        }
    }

    /// <summary>
    /// 数据库配表数据
    /// </summary>
    /// <returns></returns>
    private EnemyData GetDBData()
    {
        EnemyData nd = MogoDbManager.Instance().GetEnemyData(int.Parse(role_guid));
        return nd;
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
        if (attack_target == null)
        {
            return false;
        }

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

    #region Damage

    /// <summary>
    /// 
    /// </summary>
    /// <param name="active_attacker">主动攻击者</param>
    /// <param name="damage_go">挨打的</param>
    public void On_DamageClick(string active_attacker_role_guid, Role_AISystem damage_go)
    {
        var isown = GetIsOwn(active_attacker_role_guid,damage_go.role_guid);
        if (isown)
        {
            //Debug.LogErrorFormat("打到自己了damage_go={0}", damage_go.role_guid);
            return;
        }

        var add_hp = -2000;
        var hp = current_role_data.AddHP(add_hp);
        UpdateUIHP(hp);
        if (hp <= 0)
        {
            //die
            PlayAnimation_Die();
        }
        else
        {
            //living
            PlayAnimation_Hit(hp);
        }
    }

    private bool GetIsOwn(string active_attacker_role_guid,string hit_role_guid)
    {
        if(active_attacker_role_guid == hit_role_guid)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateUIHP(int hp)
    {
        
    }

    private void PlayAnimation_Die()
    {
        Destroy(gameObject);
    }

    private void PlayAnimation_Hit(int hp)
    {
        Debug.LogErrorFormat("名称={0} 血量={1}", role_guid, hp);
    }

    #endregion
}