using Realms;

/// <summary>
/// 信号事件主表 main event datatable
/// </summary>
public class DBGameSignalData :  RealmObject
{
    [PrimaryKey]
    public string signal_guid { get; set; }

    /// <summary>
    /// 事件名 event name
    /// </summary>
    public string signal_name { get; set; }

    /// <summary>
    /// 事件 signal_guid
    /// </summary>
    public string action_guid_json { get; set; }

    public DBGameSignalData()
    {

    }
}

/*
游戏动作表（action动作）
具体发生了什么事情。执行一个具体的游戏动作
DBGameActionData
 */