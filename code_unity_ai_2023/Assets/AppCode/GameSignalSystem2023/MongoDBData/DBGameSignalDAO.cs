using Realms;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DBGameSignalDAO : MonoBehaviour
{
    public static DBGameSignalDAO _instance;

    public static DBGameSignalDAO GetInstance()
    {
        if (_instance == null)
        {
            _instance = new DBGameSignalDAO();
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


    public void Insert(DBGameSignalData info)
    {
        GetRealm().Write(() => GetRealm().Add(info));
    }

    public DBGameSignalData GetInfo(string signal_guid)
    {
        var lst = GetRealm().All<DBGameSignalData>().Where(g => g.signal_guid == signal_guid);
        if (lst.Count() > 0)
        {
            return lst.FirstOrDefault();
        }
        else
        {
            return null;
        }
    }

    public List<DBGameSignalData> GetList()
    {
        var lst = GetRealm().All<DBGameSignalData>().ToList();
        return lst;
    }

}

