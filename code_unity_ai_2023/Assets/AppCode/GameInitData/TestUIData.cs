using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var lst = JSON_GameRoleDB.GetList();
        foreach (var item in lst)
        {
            Debug.Log(item.Role_GUID);
        }
    }

   
}
