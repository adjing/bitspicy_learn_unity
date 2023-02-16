using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;
    public Role_AISystem role_ai_system;

    [Header("Ñ¡ÔñÒ»¸ö²ãrole")]
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
        layer_ground = LayerMask.GetMask(new string[] {EnumGameObjectLayer.role});
        layer_ground = LayerMask.GetMask(new string[] {EnumGameObjectLayer.ground});
    }

    private void SetComponent()
    {
        role_ai_system = GetComponent<Role_AISystem>();
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

        if (Input.GetMouseButtonDown(1))
        {
            var vmt = GetEnemyInfo();
            if (vmt != null)
            {
                //Debug.Log("Need open the MonsterAttributePanel!");
                MonsterAttributePanel.Instance().Open();
                MonsterAttributePanel.Instance().InitData(vmt);
            }
        }
    }
    public bool CanReachPosition(Vector2 position)
    {
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.CalculatePath(position, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }

    public EnemyData GetEnemyInfo()
    {
        //int layerMask = 1 << 11;

        //RaycastHit hit;
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layer_role))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * this.hit.distance, Color.yellow);
            //Debug.LogErrorFormat("id={0} hit1={1}", layer_role, hit.collider.gameObject.layer);
            var role = hit.collider.gameObject.GetComponent<Role_AISystem>();
            EnemyData nd = MogoDbManager.Instance().GetEnemyData(int.Parse(role.role_guid));
            return nd;
        }
        else
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
            return null;
        }
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