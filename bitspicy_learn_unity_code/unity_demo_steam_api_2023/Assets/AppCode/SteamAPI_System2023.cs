using UnityEngine;

public class SteamAPI_System2023 : MonoBehaviour
{
    [Header("”¶”√ID")]
    public uint app_id = 2374170;

    void Start()
    {
        try
        {
            Steamworks.SteamClient.Init(app_id);
            ShowYourName();
        }
        catch(System.Exception e)
        {
            Debug.LogError(e);
            Debug.Log(e.Message);
        }
    }

    private void ShowYourName()
    {
        var is_logged_on = Steamworks.SteamClient.IsLoggedOn;
        var log =string.Format("IsLoggedOn:{0} Name:{1}  AppID:{2}",
            is_logged_on,
            Steamworks.SteamClient.Name, 
            Steamworks.SteamClient.AppId);
        Debug.Log(log);
    }

    private void Update()
    {
        Steamworks.SteamClient.RunCallbacks();
    }

    private void OnApplicationQuit()
    {
        Steamworks.SteamClient.Shutdown();
    }
}
