using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIBehaviour : MonoBehaviour {


	public enum FSMState
	{
		None,
		Patrol,
		Chase,
	}

	public FSMState curState;
	public float curSpeed;
	private GameObject player;

	void Initialize(){
		curState = FSMState.Patrol;	
	}

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");	
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

	void UpdatePatrolState(){

	}

	void UpdateChaseState(){
		
	}

}
