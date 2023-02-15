/*
短武器伤害触发器
刀
棍棒
剑

*/
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Trigger_ShortWeaponDamage : MonoBehaviour
{
    [Header("是否打印日志")]
    public bool ShowLog = true;
    void Start()
    {
        
    }

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
    }
}
