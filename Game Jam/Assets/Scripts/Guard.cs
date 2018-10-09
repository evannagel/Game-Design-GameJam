using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Guard : MonoBehaviour {


    public float distanceBeforeNoticing = 10f;

    private Player player;
    private AICharacterControl aiControl;

    public bool patrolling = false;


	// Use this for initialization
	void Start () {
        aiControl = GetComponent<AICharacterControl>();
        player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
        
        if(patrolling)
        {

            if (Vector3.Distance(transform.position, player.transform.position) <= distanceBeforeNoticing)
            {
                if (Misc.IsObjectInFrontOfOther(transform, player.transform))
                {
                    aiControl.SetPosition(player.transform.position);
                }
            }
        }

	}
}
