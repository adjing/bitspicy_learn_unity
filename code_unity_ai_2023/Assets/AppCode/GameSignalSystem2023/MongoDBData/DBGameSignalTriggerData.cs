using Realms;

public class DBGameSignalTriggerData : RealmObject
{
    [PrimaryKey]
    public string trigger_guid { get; set; }

    /// <summary>
    /// 1=game time;2=hero hit
    /// </summary>
    public int trigger_type { get; set; }

    /// <summary>
    /// start time
    /// </summary>
    public string start_time { get; set; }

    /// <summary>
    /// end time
    /// </summary>
    public string end_time { get; set; }

    //public string position_x { get; set; }
    //public string position_y { get; set; }
    //public string position_z { get; set; }

    /// <summary>
    /// 人物 game role,or game scene map,or game scene effect
    /// </summary>
    public string role_guid { get; set; }

    /// <summary>
    /// DBGameSignalData.signal_guid  信号主表GUID
    /// </summary>
    public string signal_guid { get; set; }

    public DBGameSignalTriggerData()
    {
    }

    public DBGameSignalTriggerData(string trigger_guid, 
        int trigger_type, 
        string start_time, 
        string end_time, 
        string role_guid
        )
    {
        this.trigger_guid = trigger_guid;
        this.trigger_type = trigger_type;
        this.start_time = start_time;
        this.end_time = end_time;
        this.role_guid = role_guid;
    }
}

/*
 在某一个时间点，某些人做了什么事情
 DBGameSignalTriggerDAO
 */