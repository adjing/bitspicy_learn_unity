using System.Collections.Generic;
using UnityEngine;

public class BirthPointManager : MonoBehaviour
{
    [Header("拖拽一些出生点")]
    public List<Transform> monster_spawn_pos_list = new List<Transform>();
    public Transform GetMonsterSpawnParent()
    {
        var spawn_parent = transform;
        if(monster_spawn_pos_list.Count > 0 )
        {
            var index = UnityEngine.Random.Range(0, monster_spawn_pos_list.Count);
            spawn_parent = monster_spawn_pos_list[index];
        }

        return spawn_parent;
    }
 
}
