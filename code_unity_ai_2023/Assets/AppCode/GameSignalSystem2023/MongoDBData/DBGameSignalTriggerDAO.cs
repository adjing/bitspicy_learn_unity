using Realms;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DBGameSignalTriggerDAO : MonoBehaviour
{
    public static DBGameSignalTriggerDAO _instance;

    public static DBGameSignalTriggerDAO GetInstance()
    {
        if (_instance == null)
        {
            _instance = new DBGameSignalTriggerDAO();
        }
        return _instance;
    }

    private Realm _realm = null;
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


    public void Insert(DBGameSignalTriggerData info)
    {
        GetRealm().Write(() => GetRealm().Add(info));
    }

    public DBGameSignalTriggerData GetInfo(string trigger_guid)
    {
        var lst = GetRealm().All<DBGameSignalTriggerData>().Where(g => g.trigger_guid == trigger_guid);
        if (lst.Count() > 0)
        {
            return lst.FirstOrDefault();
        }
        else
        {
            return null;
        }
    }

    public List<DBGameSignalTriggerData> GetList()
    {
        var lst = GetRealm().All<DBGameSignalTriggerData>().ToList();
        return lst;
    }

}

