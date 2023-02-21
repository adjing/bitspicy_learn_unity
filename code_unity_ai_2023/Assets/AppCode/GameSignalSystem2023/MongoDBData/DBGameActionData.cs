using Realms;

/// <summary>
/// 游戏动作表（action动作）
/// 具体发生了什么事情。执行一个具体的游戏动作
/// DBGameActionData
/// </summary>
public class DBGameActionData : RealmObject
{
    [PrimaryKey]
    public string action_guid { get; set; }

    /// <summary>
    /// C# action name
    /// </summary>
    public string action_name { get; set; }

    public string position_x { get; set; }
    public string position_y { get; set; }
    public string position_z { get; set; }

    public DBGameActionData()
    {

    }

}
