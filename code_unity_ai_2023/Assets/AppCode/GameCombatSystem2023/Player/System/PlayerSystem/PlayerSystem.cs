/*
玩家管理系统
1.账号信息
2 角色管理

初始化顺序:
1 database config data
2 GetComponent from Unity
*/

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家账号信息
/// </summary>
public class PlayerSystem : MonoBehaviour
{
    /// <summary>
    /// 当前角色System
    /// </summary>
    public AI_GameRoleSystem current_role_system;

    /// <summary>
    /// 当前角色
    /// </summary>
    public GameRoleData current_role_data;

    /// <summary>
    /// 角色列表
    /// </summary>
    public List<GameRoleData> role_list = new List<GameRoleData>();

    /// <summary>
    /// 当前角色GO
    /// </summary>
    public GameObject current_role_game_object;

    #region Instance
    public static PlayerSystem Instance = null;

    public static PlayerSystem Get_Instance()
    {
        return Instance;
    }

    private void Awake()
    {
        Instance = this;
    }
    #endregion


    /// <summary>
    /// data and scene view component
    /// </summary>
    public void InitGameData(GameRoleData data)
    {
        SetData(data);
        InitComponent(data);
    }

    public void InitComponent(GameRoleData data)
    {
        //玩家有一个角色默认在场景里面时就直接获取
        current_role_system = GetComponentInChildren<AI_GameRoleSystem>();

        if(current_role_system != null )
        {
            current_role_system.StartInitData(data);
            current_role_game_object = current_role_system.gameObject;
        }
        else
        {
            Debug.LogErrorFormat("current_role_system is null.role_guid={0}", data.role_guid);
        }
    }

    private void SetData(GameRoleData p)
    {
        current_role_data = p;
        role_list.Add(p);
    }

    public GameObject GetCurrentRoleGameObject()
    {
        return current_role_game_object;
    }

    public void SetAttackTarget(Transform t)
    {
        if (current_role_system != null)
        {
            current_role_system.SetAttackTarget(t);
        }
        else
        {
            Debug.LogErrorFormat("current_role_system is null.role_guid={0}", Time.time);
        }
    }
}
