using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool isSearched = false;
    public bool isHidden = false;

    public List<GameObject> items { get; set; }

    public int itemsAmount = 4;



	// Use this for initialization
	void Start () {
        items = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void AddItem(GameObject item)
    {
        if (!this.items.Contains(item) && items.Count <= itemsAmount)
        {
            this.items.Add(item);
            Debug.Log("added item : " + item.name);

            ItemUIController itemUIController = GameObject.FindObjectOfType<ItemUIController>();
            itemUIController.UpdateUI(items);

        }
    }

    public bool HasItem(GameObject item)
    {
        return items.Contains(item);
    }

    public void RemoveItem(GameObject item)
    {
        if (HasItem(item))
        {
            this.items.Remove(item);
            Debug.Log("removed item : " + item.name);

            ItemUIController itemUIController = GameObject.FindObjectOfType<ItemUIController>();
            itemUIController.UpdateUI(items);
        }
    }
}
