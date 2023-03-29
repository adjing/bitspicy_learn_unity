/*
参考文档:
https://docs.unity.com/authentication/en/manual/set-up-steam-signin

 */
using Steamworks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SteamworksNET_System : MonoBehaviour
{
    [Header("应用ID")]
    public uint app_id = 2374170;

    [Header("Unity Cloud Save")]
    public CloudSave_System CloudSave_System;

    [Header("log")]
    public Text log_txt;

    protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;
    private CallResult<NumberOfCurrentPlayers_t> m_NumberOfCurrentPlayers;
    Callback<GetAuthSessionTicketResponse_t> m_AuthTicketResponseCallback;
    HAuthTicket m_AuthTicket;
    string m_SessionTicket;

    void SignInWithSteam()
    {
        // Create the callback to receive events when the session ticket
        // is ready to use in the web API.
        m_AuthTicketResponseCallback = Callback<GetAuthSessionTicketResponse_t>.Create(OnAuthCallback);

        var buffer = new byte[1024];
        m_AuthTicket = SteamUser.GetAuthSessionTicket(buffer, buffer.Length, out var ticketSize);
        Array.Resize(ref buffer, (int)ticketSize);

        // The ticket is not ready yet, wait for OnAuthCallback.
        m_SessionTicket = BitConverter.ToString(buffer).Replace("-", string.Empty);
    }

    void OnAuthCallback(GetAuthSessionTicketResponse_t callback)
    {
        // Call Unity Authentication SDK to sign in or link with Steam.
        Debug.Log("Steam Login success. Session Ticket: " + m_SessionTicket);

        if (CloudSave_System != null)
        {
            CloudSave_System.StartInitializeUnityServices(m_SessionTicket);
        }
    }

    void Start()
    {
        if (SteamManager.Initialized)
        {
            SignInWithSteam();

            string name = SteamFriends.GetPersonaName();
            uint appID = SteamUtils.GetAppID().m_AppId;
            Debug.Log(name);
            Debug.Log("app_id="+appID.ToString());


            //var name2 = Steamworks.SteamClient.ConnectToGlobalUser

            var is_logged_on = Steamworks.SteamUser.BLoggedOn();
            var steam_account_id = Steamworks.SteamUser.GetSteamID().GetAccountID().m_AccountID.ToString();
            //
            var log = string.Format("name:{0}  steam_account_id:{1} session_id={2} is_logged_on={3}",
           name,
           steam_account_id,
           m_SessionTicket,
           is_logged_on
           );

           ShowLog(log);
        }
    }

    private void ShowLog(string msg)
    {
        Debug.Log(msg);
        if (log_txt != null)
        {
            log_txt.text = msg;
        }
    }

    private void OnEnable()
    {
        if (SteamManager.Initialized)
        {
            m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
        }

        if (SteamManager.Initialized)
        {
            m_NumberOfCurrentPlayers = CallResult<NumberOfCurrentPlayers_t>.Create(OnNumberOfCurrentPlayers);
        }
    }

    private void OnGameOverlayActivated(GameOverlayActivated_t pCallback)
    {
        if (pCallback.m_bActive != 0)
        {
            Debug.Log("Steam Overlay has been activated");
        }
        else
        {
            Debug.Log("Steam Overlay has been closed");
        }
    }

    private void OnNumberOfCurrentPlayers(NumberOfCurrentPlayers_t pCallback, bool bIOFailure)
    {
        if (pCallback.m_bSuccess != 1 || bIOFailure)
        {
            Debug.Log("There was an error retrieving the NumberOfCurrentPlayers.");
        }
        else
        {
            Debug.Log("The number of players playing your game: " + pCallback.m_cPlayers);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SteamAPICall_t handle = SteamUserStats.GetNumberOfCurrentPlayers();
            m_NumberOfCurrentPlayers.Set(handle);
            Debug.Log("Called GetNumberOfCurrentPlayers()");
        }
    }
}

/*
    private int CoinCount = 0;
    private void SaveCoin()
    {
        if (SteamManager.Initialized)
        {
            SteamUserStats.GetStat("api_game_coin",out CoinCount);
            CoinCount++;
            //
            SteamUserStats.SetStat("api_game_coin", CoinCount);
            SteamUserStats.StoreStats();

            ShowLog("api_game_coin="+ CoinCount.ToString());
            //SteamUserStats.GetAchievement("api_game_coin", out bool ach_completed);
            //if (!ach_completed)
            //{
            //    ShowLog("ach_completed=false");
            //    SteamUserStats.SetAchievement("api_game_coin");
            //    SteamUserStats.StoreStats();
            //}
            //else
            //{
            //    ShowLog("ach_completed=true");

            //}
        }
    }
 */