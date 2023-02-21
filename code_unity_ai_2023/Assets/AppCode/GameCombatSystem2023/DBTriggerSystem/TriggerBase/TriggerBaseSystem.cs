using UnityEngine;

[System.Serializable]
public class TriggerBaseSystem : MonoBehaviour
{
    [Header("Role GUID")]
    public string role_guid = "1001";
    public void InitData(string p_role_guid)
    {
        role_guid = p_role_guid;
    }
 
}
