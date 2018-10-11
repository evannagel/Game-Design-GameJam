using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAIBehaviour : MonoBehaviour
{
    public enum FSMState
    {
        None,
        Patrol,
        Chase,
    }

    [SerializeField]
    private List<Transform> wanderPoints = new List<Transform>();
    [SerializeField][Tooltip("A threshold that defines how far we can be from our actual point to be at our destination.")]
    private float destinationReachedThreshold = 10.0f;

	public FSMState curState;
	public bool playerInView;
	public int minAngle = -57;
	public int maxAngle = 57;
	public bool targetFound = false;
	public float targetLostDistance = 15f;

	private Vector3 toPlayer;
    private GameObject player;
	private Player playerCode;
	private Transform playerTransform;

    public int nodeCounter = 0;

    private NavMeshAgent enemyAgent;

	void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
		playerCode = player.GetComponent<Player> ();
		playerTransform = player.GetComponent<Transform> ();
        enemyAgent = GetComponent<NavMeshAgent>();
        curState = FSMState.Patrol;
	}
	
	// Update is called once per frame
	void Update () {
		CanSeePlayer ();

        if (Application.isEditor) { DrawRoute(); }

		if (!playerCode.isHidden && playerInView) {
			targetFound = true;
		}

		if(playerCode.isHidden || Vector3.Distance(transform.position, playerTransform.position) >= targetLostDistance && !playerInView)
		{
			targetFound = false;	
		}

		switch (curState) 
		{
		case FSMState.Patrol:
			UpdatePatrolState ();
			break;
		case FSMState.Chase:
			UpdateChaseState ();
			break;
		}
	}

	void UpdatePatrolState(){

		if (!targetFound) {
			if (wanderPoints.Count <= 0) {
				throw new NullReferenceException ("No wander point registered with this enemy!");
			}
			if (enemyAgent.destination == transform.position) {
				SetNewTarget (wanderPoints [nodeCounter].position);
				return;
			}
			if (enemyAgent.remainingDistance <= destinationReachedThreshold) {
				SetNewTarget (GetNewPointFromList ());
			}
			curState = FSMState.Patrol;
		} 

		if (!playerCode.isHidden && playerInView)
		{
			targetFound = false;
			transform.LookAt (playerTransform);
			enemyAgent.SetDestination (playerTransform.position);
			curState = FSMState.Chase;
		}       
    }

    void UpdateChaseState() {
		if (!targetFound)
        {
            curState = FSMState.Patrol;
        }
		if (!playerCode.isHidden && playerInView)
        {
			targetFound = false;
			transform.LookAt (playerTransform);
			enemyAgent.SetDestination(playerTransform.position);
            curState = FSMState.Chase;
        }
    }

    /// <summary>
    /// Sets the target for the navmesh agent
    /// </summary>
    /// <param name="point">The new target point.</param>
    private void SetNewTarget(Vector3 point)
    {
        enemyAgent.SetDestination(point);
        StartCoroutine(PathCheckCoroutine());
    }

    /// <summary>
    /// Gets a new point from the wander points list.
    /// </summary>
    /// <returns>The new point as a <see cref="Vector3"/></returns>
    private Vector3 GetNewPointFromList()
    {
        nodeCounter++;
        if (nodeCounter >= wanderPoints.Count) { nodeCounter = 0; }
        return wanderPoints[nodeCounter].position;
    }

    /// <summary>
    /// A coroutine that waits untill we have a path, and then checks if the path is valid.
    /// If not, a new point will be chosen and the invalid point is removed.
    /// </summary>
    private IEnumerator PathCheckCoroutine()
    {
        //WaitUntill requires a delegate, so we check if the property is equal to true, instead of just using the property
        yield return new WaitUntil(() => enemyAgent.hasPath == true);
        if(enemyAgent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            wanderPoints.RemoveAt(nodeCounter);
            SetNewTarget(GetNewPointFromList());
        }
    }

    /// <summary>
    /// Draws the route the player will traverse
    /// </summary>
    private void DrawRoute()
    {
        for(int i = 0; i < wanderPoints.Count; i++)
        {
            if(i == wanderPoints.Count - 1)
            {
                Debug.DrawLine(wanderPoints[i].position, wanderPoints[0].position, Color.cyan);
            }
            else
            {
                Debug.DrawLine(wanderPoints[i].position, wanderPoints[i + 1].position, Color.cyan);
            }
        }
    }

	void CanSeePlayer(){
		toPlayer = playerTransform.position - transform.position;
		float angleToPlayer = (Vector3.Angle (transform.forward, toPlayer));
		//ex: -90 and 90 = 180 degrees field of view. Human FOV is 114.(-57, 57)
		if (angleToPlayer >= minAngle && angleToPlayer <= maxAngle) {
			Ray enemyToPlayerRay = new Ray (transform.position, toPlayer);
			float rayRange = 10f;
			Debug.DrawRay (enemyToPlayerRay.origin, enemyToPlayerRay.direction*rayRange, Color.blue);
			RaycastHit hit;
			if(Physics.Raycast(enemyToPlayerRay, out hit, rayRange)){
				if (hit.collider.CompareTag ("Player")) {					
					playerInView = true;
				} else {
					playerInView = false;
				}
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.CompareTag ("Player")) {					
			SceneManager.LoadScene (SceneManager.GetSceneByBuildIndex (0).name);
		}
	}
}
