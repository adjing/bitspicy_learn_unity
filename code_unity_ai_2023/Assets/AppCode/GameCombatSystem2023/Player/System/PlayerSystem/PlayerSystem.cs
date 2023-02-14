/*
玩家管理系统
1.账号信息
2 角色管理
 
*/

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家账号信息
/// </summary>
public class PlayerSystem : MonoBehaviour
{
    /// <summary>
    /// 当前角色
    /// </summary>
    public GameRole_Data current_role_data;

    /// <summary>
    /// 角色列表
    /// </summary>
    public List<GameRole_Data> role_list = new List<GameRole_Data>();

    //public int health;
    //public int defend;
    //public int energy;

    private void Start()
    {
        InitHeroData();
    }

    private void InitHeroData()
    {
        GameRole_Data data = new GameRole_Data();
        data.role_type = "1";
        data.role_guid = "101";
        data.role_name = "hero 101";

        data.hp= 100;
        data.defend = 100;
        data.energy= 100;

        SetData(data);
    }

    public void SetData(GameRole_Data p)
    {
        current_role_data = p;
        role_list.Add(p);
    }
}
