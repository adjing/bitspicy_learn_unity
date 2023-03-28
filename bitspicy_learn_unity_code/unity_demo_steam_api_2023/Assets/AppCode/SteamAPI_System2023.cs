using Steamworks;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.Windows;

public class SteamAPI_System2023 : MonoBehaviour
{
    [Header("应用ID")]
    public uint app_id = 2374170;

    [Header("log")]
    public Text log_txt;

    private int count = 0;
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
        ShowLog(log);
        Debug.Log(log);
    }

    private void Update()
    {
        Steamworks.SteamClient.RunCallbacks();
    }

    private void ShowLog(string msg)
    {
        if(log_txt != null)
        {
            log_txt.text = msg;
        }
    }

    public void IsCloudEnabled()
    {
        ConsoleWriteLine($"SteamRemoteStorage.IsCloudEnabled: {SteamRemoteStorage.IsCloudEnabled}");
        ConsoleWriteLine($"SteamRemoteStorage.IsCloudEnabledForAccount: {SteamRemoteStorage.IsCloudEnabledForAccount}");
        ConsoleWriteLine($"SteamRemoteStorage.IsCloudEnabledForApp: {SteamRemoteStorage.IsCloudEnabledForApp}");
    }

    public void Quotas()
    {
        ConsoleWriteLine($"SteamRemoteStorage.QuotaBytes: {SteamRemoteStorage.QuotaBytes}");
        ConsoleWriteLine($"SteamRemoteStorage.QuotaRemainingBytes: {SteamRemoteStorage.QuotaRemainingBytes}");
        ConsoleWriteLine($"SteamRemoteStorage.QuotaUsedBytes: {SteamRemoteStorage.QuotaUsedBytes}");
    }

    private void ConsoleWriteLine(string v)
    {
        Debug.Log(v);
    }

    public void SaveDataToDB()
    {
 
    }

    private string remote_file_name = "player_data.txt";
    public void SaveDataToDB2()
    {
        Quotas();
        IsCloudEnabled();

        PlayerData_Com info = new PlayerData_Com();
        info.user_guid = "100";
        info.user_name = "zhang san";
        info.description = "hero ge";
        info.level = 2;

        var json = JsonUtility.ToJson(info);
        Debug.Log(json);

        byte[] array = Encoding.ASCII.GetBytes(json);

        var written = SteamRemoteStorage.FileWrite(remote_file_name, array);

        Assert.IsTrue(written);
        Assert.IsTrue(SteamRemoteStorage.FileExists(remote_file_name));
        Assert.AreEqual(SteamRemoteStorage.FileSize(remote_file_name), array.Length);

        count++;

        var log = string.Format("save click={0} time:{1}",
            count,
           Time.time
           );

        Debug.Log(log);

    }

    public void On_GetDataFromDB()
    {

        byte[] array = SteamRemoteStorage.FileRead("player_data.txt");

        if (array == null)
        {
            Debug.LogError("data array is null");
            return;
        }

        string json= Encoding.ASCII.GetString(array);
        if(string.IsNullOrEmpty(json))
        {
            Debug.LogError("json text is null");
            return;
        }
        Debug.Log(json);
        var json_go = JsonUtility.FromJson<PlayerData_Com>(json);

        var log = string.Format("user_guid={0} user_name:{1}",
            json_go.user_guid,
            json_go.user_name
           );

        Debug.Log(log);

    }

    private void On_CheckUser()
    {
      
        // Client sends ticket data to server somehow
        var ticket = SteamUser.GetAuthSessionTicket();

        // server listens to event
        SteamServer.OnValidateAuthTicketResponse += (steamid, ownerid, rsponse) =>
        {
            if (rsponse == AuthResponse.OK)
            {
                //TellUserTheyCanBeOnServer(steamid);
                var log = string.Format("TellUser steamid={0} ownerid={1} rsponse={2}", steamid, ownerid, rsponse);
                Debug.Log(log);
            }
            else
            {
                //KickUser(steamid);
                var log = string.Format("KickUser steamid={0} ownerid={1} rsponse={2}", steamid, ownerid, rsponse);
                Debug.Log(log);

            }
        };

        // server gets ticket data from client, calls this function.. which either returns
        // false straight away, or will issue a TicketResponse.
        if (!SteamServer.BeginAuthSession(ticket.Data, SteamClient.SteamId))
        {
            //KickUser(clientSteamId);
            var log = string.Format("KickUser2 steamid={0}", SteamClient.SteamId);
            Debug.Log(log);
        }
        else
        {
            var log = string.Format("BeginAuthSession OK steamid={0}", SteamClient.SteamId);
            Debug.Log(log);
        }

        //
        // Client is leaving, cancels their ticket OnValidateAuth is called on the server again
        // this time with AuthResponse.AuthTicketCanceled
        //
        ticket.Cancel();
    }

    private void OnApplicationQuit()
    {
        Steamworks.SteamClient.Shutdown();
    }
}

[System.Serializable]
public struct PlayerData_Com
{
    public string user_guid;
    public string user_name;
    public string description;
    public int level;
}

/*
 添加统计
 static bool AddStat( string name, int amount = 1 )

 */