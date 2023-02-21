/*
level manager
*/
using System;
using UnityEngine;


public class FightLevel_Manager : MonoBehaviour
{
    [Header("hero guid")]
    public string select_hero_role_guid= "6400008";

    [Header("monster guid")]
    public string first_monster_role_guid = "6400001";

    [Header("Player System")]
    public PlayerSystem m_player_system;

    [Header("Spawn Monster System")]
    public AutoSpawnMonsterSystem m_spawn_monster_system;

    [Header("Birth Point Manager")]
    public BirthPointManager m_birth_point_system;

    void Start()
    {
        CreatePlayer(select_hero_role_guid);
        SpawnMonster(first_monster_role_guid);
    }

    private void SpawnMonster(string role_guid)
    {
        var d = GetGameData(role_guid);
        if (d != null && m_spawn_monster_system != null)
        {
            
            d.parent = GetSpawnParentTransform();
            d.on_spawn_success_click = On_SpawnMonsterSuccessClick;
            m_spawn_monster_system.On_DoTask(d);

            Debug.LogErrorFormat("parent={0} addressable_guid={1}", d.parent.name,d.addressable_guid);
        }
    }

    private void On_SpawnMonsterSuccessClick(GameObject obj)
    {
        if(PlayerSystem.Get_Instance() == null)
        {
            Debug.LogErrorFormat("PlayerSystem Get_Instance is null {0}", Time.time);
            return;
        }

        var role = obj.GetComponent<AI_GameRoleSystem>();
        if (role != null)
        {
            var d = GetGameData(first_monster_role_guid);
            role.StartInitData(d);
            //set hero go
            role.SetAttackTarget(PlayerSystem.Get_Instance().GetCurrentRoleGameObject().transform);
        }

        //main player target
        m_player_system.SetAttackTarget(obj.transform);
    }

    private Transform GetSpawnParentTransform()
    {
        if (m_birth_point_system == null)
        {
            Debug.LogErrorFormat("GetSpawnParentTransform {0}", Time.time);
            return transform;
        }

        if (m_birth_point_system != null)
        {
            return m_birth_point_system.GetMonsterSpawnParent();
        }

        return transform; 
    }  

    private void CreatePlayer(string role_guid)
    {
        if(m_player_system == null)
        {
            Debug.LogError("m_player_system is null");
            return;
        }

        var d = GetGameData(role_guid);
        m_player_system.InitGameData(d);
    }

    /// <summary>
    /// GetGameData
    /// </summary>
    /// <param name="role_guid"></param>
    /// <returns></returns>
    public GameRoleData GetGameData(string role_guid)
    {
        var dbrow = GetMongoDBConfigData(role_guid);

        //
        GameRoleData data = new GameRoleData();
        data.role_type = dbrow.role_type;
        data.role_guid = dbrow.id.ToString();
        data.role_name = dbrow.enemy_chief;
        data.addressable_guid = dbrow.model.ToString();

        data.hp = dbrow.health;
        data.defend = 100;
        data.energy = 100;

        return data;
    }

    /// <summary>
    /// GetMongoDBConfigData
    /// </summary>
    /// <returns></returns>
    private DBGameRoleData GetMongoDBConfigData(string role_guid)
    {
        return null;
    }
}
