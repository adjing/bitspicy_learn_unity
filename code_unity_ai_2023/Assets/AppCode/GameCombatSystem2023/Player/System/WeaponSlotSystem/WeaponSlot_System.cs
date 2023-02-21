using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 武器槽
/// </summary>
[System.Serializable]
public class WeaponSlot_System
{
   public List<TriggerBaseSystem> trigger_list = new List<TriggerBaseSystem>();
    public void StartInitData(GameRoleData d)
    {
        for(int i=0;i<trigger_list.Count;i++)
        {
            var item = trigger_list[i];
            if(item != null)
            {
                item.InitData(d.role_guid);
            }
        }
    }

  
}
