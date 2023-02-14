using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 角色系统-挂英雄或怪物身上
/// </summary>
public class GameRole_System : MonoBehaviour
{
    public RoleType m_RoleType = RoleType.Monster;
    public Animator m_animator;
    public string game_object_guid;
    // Adjust the speed for the application.
    public float speed = 1.0f;
    public Text m_ui_role_name;
    public GameRole_Data data;

    public void Set_GameObjectGUID(string guid)
    {
        game_object_guid = guid;
    }

    public string Get_GameObjectGUID()
    {
        return game_object_guid;
    }

    public void Set_Data(GameRole_Data p)
    {
        data.Set_Data(Get_GameObjectGUID(),p);
        ShowUIRoleName(p);
    }

    private void ShowUIRoleName(GameRole_Data p)
    {
        if(m_ui_role_name != null)
        {
            m_ui_role_name.text = p.role_name;
        }
    }

    public void Update_HP(int hp)
    {
        
    }

    public void On_DamageClick()
    {
        m_animator.SetInteger("cmd", 1);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.LogFormat("{0} 碰到主角了", Time.time);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("tag=" + collision.collider.tag + "role type="+ m_RoleType.ToString());
        //is main player
        if (m_RoleType == RoleType.Hero)
        {
            if (collision.collider.tag == "monster")
            {
                Debug.LogFormat("{0} GameOver 碰到monster 了2", Time.time);
            }
        }
    }
}
//GameRole_Data

public enum RoleType:int
{
    Hero,
    Monster,
}