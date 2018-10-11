using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool isSearched = false;
    public bool isHidden = false;

    public List<GameObject> items { get; set; }



	// Use this for initialization
	void Start () {
        items = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void AddItem(GameObject item)
    {
        if (!this.items.Contains(item))
        {
            this.items.Add(item);
            Debug.Log("added item : " + item.name);
        }
    }
}
