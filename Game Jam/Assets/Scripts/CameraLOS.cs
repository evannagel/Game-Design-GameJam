using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLOS : MonoBehaviour {

    private Player player;

    public float distanceBeforeNoticing = 8f;


    // Use this for initialization
    void Start () {
        player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {


       if(player.isSearched)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= distanceBeforeNoticing)
            {
                if (Misc.IsInLineOfSight(transform, player.transform, 10))
                {
                    Debug.Log("In LOS!");
                }
            }
        }
    }
}
