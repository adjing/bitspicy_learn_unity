using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;

public class CloudSave_System : MonoBehaviour
{
    const int k_NewPlayerLevel = 1;
    const int k_NewPlayerCoin = 10;
    const string k_PlayerLevelKey = "AB_TEST_PLAYER_LEVEL";
    const string k_PlayerCoinKey = "AB_TEST_PLAYER_COIN";

    Dictionary<string, int> m_CachedCloudData = new Dictionary<string, int>
    {
        { k_PlayerLevelKey, 0 },
        { k_PlayerCoinKey, 0 }
    };

    async public void StartInitializeUnityServices(string steam_session_ticket)
    {
        try
        {
            await InitializeUnityServices(steam_session_ticket);
            await LoadServicesData();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    async Task InitializeUnityServices(string steam_session_ticket)
    {
        await UnityServices.InitializeAsync();
        if (this == null)
        {
            return;
        }

        var log_1 = string.Format("Services Initialized.IsSignedIn={0}", AuthenticationService.Instance.IsSignedIn);
        Debug.Log(log_1);

        //await AuthenticationService.Instance.LinkWithSteamAsync(steam_session_ticket);
        //await AuthenticationService.Instance.SignInWithSteamAsync(steam_session_ticket);
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            Debug.Log("Signing in...");
            //匿名登录
            //await AuthenticationService.Instance.SignInAnonymouslyAsync();

            try
            {
                await AuthenticationService.Instance.SignInWithSteamAsync(steam_session_ticket);
            }
            catch(Exception e)
            {
                Debug.LogException(e);
            }

            if (this == null)
            {
                return;
            }
        }
        else
        {
            await AuthenticationService.Instance.LinkWithSteamAsync(steam_session_ticket);
        }

        //LocalSaveManager.AddNewPlayerId(AuthenticationService.Instance.PlayerId);
        Debug.Log($"Player id: {AuthenticationService.Instance.PlayerId}");
    }

    async Task LoadServicesData()
    {
        await Task.WhenAll(
            LoadAndCacheData()
        );
    }

    public async Task LoadAndCacheData()
    {
        try
        {
            var savedData = await CloudSaveService.Instance.Data.LoadAllAsync();

            // Check that scene has not been unloaded while processing async wait to prevent throw.
            if (this == null) return;

            var missingData = new Dictionary<string, object>();
            if (savedData.ContainsKey(k_PlayerLevelKey))
            {
                m_CachedCloudData[k_PlayerLevelKey] = int.Parse(savedData[k_PlayerLevelKey]);
            }
            else
            {
                missingData.Add(k_PlayerLevelKey, k_NewPlayerLevel);
                m_CachedCloudData[k_PlayerLevelKey] = k_NewPlayerLevel;
            }

            if (savedData.ContainsKey(k_PlayerCoinKey))
            {
                m_CachedCloudData[k_PlayerCoinKey] = int.Parse(savedData[k_PlayerCoinKey]);
            }
            else
            {
                missingData.Add(k_PlayerCoinKey, k_NewPlayerCoin);
                m_CachedCloudData[k_PlayerCoinKey] = k_NewPlayerCoin;
            }

            if (missingData.Count > 0)
            {
                await SaveUpdatedData(missingData);
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    async Task SaveUpdatedData(Dictionary<string, object> data)
    {
        try
        {
            await CloudSaveService.Instance.Data.ForceSaveAsync(data);
        }
        catch (Exception e)
        {
            Debug.LogError("Save data error:" + e.Message);
            Debug.LogException(e);
        }
    }

    public void UpdateCachedPlayerLevel(int newLevel)
    {
        m_CachedCloudData[k_PlayerLevelKey] = newLevel;
    }
}

/*
 需要安装的包
    "com.unity.services.cloudsave": "2.0.1",

 */

//https://docs.unity.com/cloud-save/en/manual
//https://docs.unity.com/ugs-use-cases/en/manual/DownloadAndInstallation


//https://docs.unity.com/authentication/en/manual/platform-signin-steam