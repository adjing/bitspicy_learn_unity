using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;
    RaycastHit hit;
    public NavMeshAgent navMeshAgent;
    public int state = 0;
    public AnimationClip idleAnimationClip;
    public AnimationClip walkAnimationClip;
    public AnimationClip runAnimationClip;
    public AnimationClip jumpAnimationClip;
    public Animator playerAnimator;
    private Vector3 destination;
    private bool isWalk = false;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator.SetInteger("state", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            state = 1;
            //navMeshAgent.SetDestination(GetHitPosition());
            //Debug.Log(gameObject.transform.position);

            destination = GetHitPosition();
            navMeshAgent.destination = destination;
            isWalk = true;
            playerAnimator.SetInteger("state", 1);

        }
        if (navMeshAgent.remainingDistance<0.1f&&isWalk==true)
        {
            isWalk = false;
            Debug.Log("NavMeshAgent Stop!");           
            state = 0;
            playerAnimator.SetInteger("state", 0);
        }
        if (Input.GetMouseButtonDown(1))
        {
            var vmt = GetEnemyInfo();
            if(vmt != null)
            {
                Debug.Log("Need open the MonsterAttributePanel!");
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
        RaycastHit myHit;
        Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out myHit, 1000);
        if (myHit.collider.gameObject.layer==13)
        {
            EnemyInfo ei = myHit.collider.gameObject.GetComponent<EnemyInfo>();
            Debug.Log("I already hit the monster!");
            EnemyData nd = MogoDbManager.Instance().GetEnemyData(ei.enemyId);
            return nd;
        }
        else
        {
            return null;
        }
    }
    public Vector3 GetHitPosition()
    {
        Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition),out hit,1000);

        if(hit.collider == null)
        {
            return gameObject.transform.position;
        }

        if (hit.collider.gameObject.layer == 12)
        {
            Debug.Log("Hit the ground");
            return hit.point;
        }

        return gameObject.transform.position;

    }
    public enum PlayerState
    {
        IDLE=0,
        WALK=1,
        RUN=2,
        JUMP=3
    }
}
