using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;
    public AI_GameRoleSystem role_ai_system;

    [Header("角色层")]
    public LayerMask layer_role;

    public LayerMask layer_ground;


    private RaycastHit hit;
    public NavMeshAgent navMeshAgent;

    private Vector3 destination;
    private bool isWalk = false;
    //
    void Start()
    {
        SetComponent();
        SetLayerMask();
        PlayIdle();
    }

    private void SetLayerMask()
    {
        layer_ground = LayerMask.GetMask(new string[] { EnumGameObjectLayer.role });
        layer_ground = LayerMask.GetMask(new string[] { EnumGameObjectLayer.ground });
    }

    private void SetComponent()
    {
        role_ai_system = GetComponent<AI_GameRoleSystem>();
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            //navMeshAgent.SetDestination(GetHitPosition());
            //Debug.Log(gameObject.transform.position);

            destination = GetHitPosition();
            navMeshAgent.destination = destination;
            isWalk = true;
            PlayWalk();
        }

        if (navMeshAgent.remainingDistance < 0.1f && isWalk == true)
        {
            isWalk = false;
            //Debug.Log("NavMeshAgent Stop!");
            PlayIdle();
        }

       

    }
    public bool CanReachPosition(Vector2 position)
    {
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.CalculatePath(position, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }

    public DBGameRoleData GetEnemyInfo()
    {
        return null;
        //int layerMask = 1 << 11;

        //RaycastHit hit;
        //if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layer_role))
        //{
        //    //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * this.hit.distance, Color.yellow);
        //    Debug.LogErrorFormat("id={0} hit1={1}", layer_role, hit.collider.gameObject.layer);
        //    var role = hit.collider.gameObject.GetComponent<AI_GameRoleSystem>();
        //    if (role != null)
        //    {
        //        DBGameRoleData nd = MogoDbManager.Instance().GetEnemyData(int.Parse(role.role_guid));
        //        if (nd == null)
        //        {
        //            Debug.LogErrorFormat("role_guid={0} ������DB!", role.role_guid);
        //        }
        //        return nd;
        //    }
        //    else
        //    {
        //        Debug.LogErrorFormat("id={0} hit name={1}", layer_role, hit.collider.name);
        //        return null;
        //    }
           
        //}
        //else
        //{
        //    //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        //    //Debug.Log("Did not Hit");
        //    return null;
        //}
    }

    public Vector3 GetHitPosition()
    {
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layer_ground))
        {
            return hit.point;
        }
        else
        {
            return gameObject.transform.position;
        }

        //Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 1000);

        //if (hit.collider == null)
        //{
        //    return gameObject.transform.position;
        //}

        //if (hit.collider.gameObject.layer == 12)
        //{
        //    //Debug.Log("Hit the ground");
        //    return hit.point;
        //}

        //return gameObject.transform.position;

    }

    #region Animator
    private void PlayIdle()
    {
        if (role_ai_system == null)
        {
            SetComponent();
            return;
        }

        role_ai_system.PlayIdle();
    }

    private void PlayWalk()
    {
        if (role_ai_system == null)
        {
            SetComponent();
            return;
        }

        role_ai_system.PlayWalk();
    }
    #endregion
}

//20230215
//Assets/ModularRPGHeroesHP/Prefabs/BasicCharacters/SwordShieldKnight.prefab