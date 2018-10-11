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

	protected override void Initialize(){
		curState = FSMState.Patrol;
	
	}



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
