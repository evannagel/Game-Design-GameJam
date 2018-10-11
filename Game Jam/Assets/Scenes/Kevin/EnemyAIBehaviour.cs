using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIBehaviour : MonoBehaviour {


	public enum FSMState
	{
		None,
		Patrol,
		Chase,
	}

	public FSMState curState;
	public float curSpeed;
	public bool playerInView;
	public int minAngle = -57;
	public int maxAngle = 57;
	public Transform playerTransform;
	public Player player;

	private NavMeshAgent agent;
	private Vector3 toPlayer;

	void Initialize(){
	}

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		curState = FSMState.Patrol;	
	}
	
	// Update is called once per frame
	void Update () {
		CanSeePlayer ();

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
		if (player.isHidden) 
		{
			//patrol
			agent.isStopped = true;
			curState = FSMState.Patrol;
		}
		if (!player.isHidden && playerInView)
		{
			agent.isStopped = false;
			agent.SetDestination (toPlayer);
			curState = FSMState.Chase;
		}
	}

	void UpdateChaseState(){
		if (player.isHidden) 
		{
			//patrol
			agent.isStopped = true;
			curState = FSMState.Patrol;
		}
		if (!player.isHidden && playerInView)
		{
			agent.isStopped = false;
			agent.SetDestination (toPlayer);
			curState = FSMState.Chase;
		}
	}

	void CanSeePlayer(){
		toPlayer = playerTransform.position - transform.position;
		float angleToPlayer = (Vector3.Angle (transform.forward, toPlayer));
		Debug.Log ("CanSeePlayer()");
		//ex: -90 and 90 = 180 degrees field of view. Human FOV is 114.(-57, 57)
		if (angleToPlayer >= minAngle && angleToPlayer <= maxAngle) {
			Debug.Log ("In angle");
			Ray enemyToPlayerRay = new Ray (transform.position, toPlayer);
			float rayRange = 10f;
			Debug.DrawRay (enemyToPlayerRay.origin, enemyToPlayerRay.direction*rayRange, Color.blue);
			RaycastHit hit;
			if(Physics.Raycast(enemyToPlayerRay, out hit, rayRange)){
				Debug.Log ("hit something");
				if(hit.collider.CompareTag("Player")){
					playerInView = true;
				}
			}
		}
	}
}
