/*
DBGameRoleData = database config,纯数据
GameRoleData = 游戏进行中的数据结构，数据+ 游戏可视化组件
*/
using Realms;

/// <summary>
/// DB 角色配置 英雄，怪，NPC
/// </summary>
public class DBGameRoleData : RealmObject
{
    /// <summary>
    /// 敌方id
    /// </summary>
    [PrimaryKey]
    public int id { get; set; }


    ///// <summary>
    ///// 可视化预制件ID,通常指模型FBX
    ///// </summary>
    //public string role_prefab_guid { get; set; }

    /// <summary>
    /// 敌方首领名称
    /// </summary>
    public string enemy_chief { get; set; }
    /// <summary>
    /// 怪物icon
    /// </summary>
    public int icon { get; set; }

    /// <summary>
    /// 调用的模型 role_prefab_guid
    /// </summary>
    public int model { get; set; }
    /// <summary>
    /// 攻击
    /// </summary>
    public int attack { get; set; }
    /// <summary>
    /// 防御
    /// </summary>
    public int defend { get; set; }
    /// <summary>
    /// 血量
    /// </summary>
    public int health { get; set; }
    /// <summary>
    /// 等级
    /// </summary>
    public int level { get; set; }
    /// <summary>
    /// 星级（1.一星 2.二星 3.三星 4.四星 5.五星）
    /// </summary>
    public int star { get; set; }


    /// <summary>
    /// 角色类型(1=hero 2=monster 3=npc)
    /// </summary>
    public string role_type { get; set; }

    public DBGameRoleData()
    {

    }

    public DBGameRoleData(int id, string enemy_chief, int icon, int model, int attack, int defend, int health, int level, int star)
    {
        this.id = id;
        this.enemy_chief = enemy_chief;
        this.icon = icon;
        this.model = model;
        this.attack = attack;
        this.defend = defend;
        this.health = health;
        this.level = level;
        this.star = star;
    }
}
