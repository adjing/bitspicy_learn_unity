using Realms;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DBGameActionDAO : MonoBehaviour
{
    public static DBGameActionDAO _instance;
    
    public static DBGameActionDAO Instance()
    {
        if (_instance == null)
        {
            _instance = new DBGameActionDAO();
        }
        return _instance;
    }

    private Realm _realm=null;
    public Realm GetRealm()
    {
      
        if (_realm == null)
        {
            _realm = Realm.GetInstance();
        }
           
        return _realm;
    }

    public void On_Disable()
    {
        _realm.Dispose();
    }


    public void Insert(DBGameActionData info)
    {
        GetRealm().Write(() => GetRealm().Add(info));
    }

    public DBGameActionData GetInfo(string action_guid)
    {
        var lst = GetRealm().All<DBGameActionData>().Where(g => g.action_guid == action_guid);
        if (lst.Count() > 0)
        {
            return lst.FirstOrDefault();
        }
        else
        {
            return null;
        }
    }
   
    public List<DBGameActionData> GetList()
    {
        var lst = GetRealm().All<DBGameActionData>().ToList();
        return lst;
    }
 
}
