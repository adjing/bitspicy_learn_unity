using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// AutoSpawnMonsterSystem
/// 2023
/// </summary>
public class AutoSpawnMonsterSystem : MonoBehaviour
{

    #region Instance
    public static AutoSpawnMonsterSystem Instance = null;

    public static AutoSpawnMonsterSystem Get_Instance()
    {
        return Instance;
    }
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    IEnumerator CreateMonster(GameRoleData d)
    {
        string key = d.addressable_guid;
        if (string.IsNullOrEmpty(key))
        {
            yield return null;
        }

        AsyncOperationHandle<GameObject> load_asset = Addressables.LoadAssetAsync<GameObject>(key);
        yield return load_asset;
        if (load_asset.Status == AsyncOperationStatus.Succeeded)
        {
            var op = Addressables.InstantiateAsync(key, d.parent);
            if (op.IsDone)
            {
                GameObject go = op.Result;
                go.transform.localPosition = Vector3.zero;

                if(d.on_spawn_success_click !=null)
                {
                    d.on_spawn_success_click.Invoke(go);
                }
            }
        }
    }

    public void On_DoTask(GameRoleData d)
    {
        Debug.LogFormat("{0} Create Monster ={1}", Time.time, "1");
        //int level_id = 1;
        if (string.IsNullOrEmpty(d.addressable_guid))
        {
            Debug.LogError("Create Monster,but no addressable_guid");
            return;
        }

        if (d.speed < 0.1)
        {
            d.speed = 10;
        }

        StartCoroutine(CreateMonster(d));
    }
}
