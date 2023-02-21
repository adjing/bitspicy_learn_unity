/*
DBGameRoleData = database config,纯数据
GameRoleData = 游戏进行中的数据结构，数据+ 游戏可视化组件
*/
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏中数据 GameRole_Data
/// 通过这些数据来创建英雄，怪物在场景里
/// </summary>
[System.Serializable]
public class GameRoleData
{
    public string user_guid;

    /// <summary>
    /// 编号GUID (Id)
    /// </summary>
    public string role_guid;

    /// <summary>
    /// 角色名称
    /// </summary>
    public string role_name;

    /// <summary>
    /// int 1=英雄 2=怪物
    /// </summary>
    public string role_type;

    /// <summary>
    /// 角色预制体GUID
    /// </summary>
    public string addressable_guid = string.Empty;

    /// <summary>
    /// 等级
    /// </summary>
    public int level;

    /// <summary>
    /// 当前血量
    /// </summary>
    public int hp;

    /// <summary>
    /// 防御
    /// </summary>
    public int defend;

    /// <summary>
    /// 命中率
    /// </summary>
    public int hit_rate;

    /// <summary>
    /// 能量值
    /// </summary>
    public int energy;

    /// <summary>
    /// 攻击
    /// </summary>
    public int attack;

    /// <summary>
    /// 速度(飞行,走路)
    /// </summary>
    public int speed;

    /// <summary>
    /// 当前有的skill列表
    /// </summary>
    public List<GameRoleSkill_Data> skill_guid_list;

    /// <summary>
    /// 出生的父节点
    /// </summary>
    public Transform parent;

    /// <summary>
    /// 开火点
    /// </summary>
    public Transform fire_start_transform;

    /// <summary>
    /// 攻击目标
    /// </summary>
    public Transform attack_target_transform;

    /// <summary>
    /// 实例化成功
    /// </summary>
    public Action<GameObject> on_spawn_success_click;

    public void Set_Data(string p_user_guid, GameRoleData p)
    {
        //角色数据
        //MogoDbManager.Instance().UpdataPlayerBattleDataDt
        user_guid = p_user_guid;
        role_type = p.role_type;

        addressable_guid = p.addressable_guid;
        role_name = p.role_name;
        hp = p.hp;

        //get skill
        //skill_guid_list = DB_PlayerGameData_Skill_DAO.I.GetList();
    }

    public void Set_AttackTargetTransform(Transform p)
    {
        attack_target_transform = p;
    }

    public int AddHP(int p)
    {
        hp += p;
        return hp;
    }

    public int GetHP()
    {
        return hp;
    }
}