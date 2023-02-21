/*
短武器伤害触发器
刀
棍棒
剑

*/
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Trigger_ShortWeaponDamage : TriggerBaseSystem
{
    //[Header("编号GUID")]
    //public string role_guid = "1001";

    [Header("是否打印日志")]
    public bool ShowLog = true;


    /// <summary>
    /// 注意 BoxCollider.IsTrigger = 勾选上
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(ShowLog)
        {
            Debug.LogErrorFormat("短武器伤害OnTriggerEnter hit={0}", other.name);
        }

        var role = other.GetComponent<AI_GameRoleSystem>();
        if(role != null)
        {
            role.On_DamageClick(role_guid,role);
        }
    }
}

/*
1 碰撞检测
2 调角色扣血方法
3 遭受攻击时特效，音效
4 
*/