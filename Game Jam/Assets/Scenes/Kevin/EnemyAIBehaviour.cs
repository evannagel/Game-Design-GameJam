using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

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
	public float curSpeed;
    private GameObject player;

    private int nodeCounter = 0;
    private NavMeshAgent enemyAgent;

	void Initialize(){
		curState = FSMState.Patrol;	
	}

	// Use this for initialization
	void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

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

    /// <summary>
    /// Checks to see if we've reached our destination or if we need a new destination
    /// </summary>
	void UpdatePatrolState()
    {
        if(wanderPoints.Count <= 0) { throw new NullReferenceException("No wander point registered with this enemy!"); }

        if (enemyAgent.destination == transform.position) { SetNewTarget(wanderPoints[nodeCounter].position); return; }

        if(enemyAgent.remainingDistance <= destinationReachedThreshold) { SetNewTarget(GetNewPointFromList()); }
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
        if (nodeCounter > wanderPoints.Count) { nodeCounter = 0; }
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

	void UpdateChaseState()
    {
		
	}

}
