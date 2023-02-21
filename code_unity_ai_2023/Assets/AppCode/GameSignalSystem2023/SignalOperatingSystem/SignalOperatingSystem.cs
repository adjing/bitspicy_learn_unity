using NodeCanvas.Tasks.Conditions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalOperatingSystem : MonoBehaviour
{
    [Header("º‰∏ÙºÏ≤‚ ±º‰√Î")]
    public float interval_time = 4.0f;
    private List<DBGameSignalTriggerData> db_signal_trigger_list = new List<DBGameSignalTriggerData>();
    void Start()
    {
        FillSignalTriggerData();
        StartCoroutine(RunSignalTrigger());
    }

    private void FillSignalTriggerData()
    {
        if (db_signal_trigger_list.Count == 0)
        {
            db_signal_trigger_list = DBGameSignalTriggerDAO.GetInstance().GetList();
        }
    }

    private IEnumerator RunSignalTrigger()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval_time);
            CheckTrigger();
        }
    }

    private void CheckTrigger()
    {
        for (int i = 0; i < db_signal_trigger_list.Count; i++)
        {
            var item = db_signal_trigger_list[i];
            if (item != null)
            {
                CheckTriggerOne(i, item);
            }
        }
    }

    private void CheckTriggerOne(int i, DBGameSignalTriggerData item)
    {
        var a = System.DateTime.Parse(item.start_time);
        Debug.LogErrorFormat("start_time={0}", a);
    }
}
