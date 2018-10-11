﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool isSearched = false;
    public bool isHidden = false;
    public bool foundRedKey = false;
    public bool foundGreenKey = false;
    public bool foundGuitar = false;
    public bool foundCup = false;

    public bool hasKeys = false;
    public bool hasCodes = false;
    public bool hasCodeOne = false;
    public bool hasCodeTwo = false;

    public bool startLookingForCodes = false;


    public List<GameObject> items { get; set; }

    public int itemsAmount = 6;

    [SerializeField]
    private int currentObjectiveNumber = 0;

    private ObjectiveUIController objectiveUIController;


    // Use this for initialization
    void Start () {
        items = new List<GameObject>();
        objectiveUIController = GameObject.FindObjectOfType<ObjectiveUIController>();

        objectiveUIController.SetText("Find the keys!");
    }

    // Update is called once per frame
    void Update () {

	}

    public void AddItem(GameObject item)
    {
        if (!this.items.Contains(item))
        {
            this.items.Add(item);
            ItemUIController itemUIController = GameObject.FindObjectOfType<ItemUIController>();
            itemUIController.UpdateUI(items);

            HandlePickup(item);

        } else
        {
            Debug.Log(!this.items.Contains(item));
            Debug.Log(items.Count <= itemsAmount);
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

    public void AdvanceObjective()
    {
        if (foundRedKey && !foundGreenKey && !hasKeys)
            objectiveUIController.SetText("Find the Green key!");
        else if (!foundRedKey && foundGreenKey && !hasKeys)
            objectiveUIController.SetText("Find the Red key!");
        else if(foundRedKey && foundGreenKey && !hasKeys)
        {
            objectiveUIController.SetText("Find the Guitar & Cup!");
            foundRedKey = false;
            foundGreenKey = false;
            hasKeys = true;
        }

        if(hasKeys)
        {
            if (foundGuitar && !foundCup && !hasCodeOne && !hasCodeTwo)
                objectiveUIController.SetText("Find the Cup!");
            else if (!foundGuitar && foundCup && !hasCodeOne && !hasCodeTwo)
                objectiveUIController.SetText("Find the Guitar!");
            else if (foundGuitar && foundCup && !hasCodeOne && !hasCodeTwo)
            {
                objectiveUIController.SetText("Find the Codes!");
                hasKeys = false;
                startLookingForCodes = true;
            }

        }

        if(!foundRedKey && !foundGreenKey && !hasKeys && startLookingForCodes)
        {
            if (hasCodeOne && !hasCodeTwo)
                objectiveUIController.SetText("Find Code One!");
            else if (!hasCodeOne && hasCodeTwo)
                objectiveUIController.SetText("Find Code Two!");
            else if (hasCodeOne && hasCodeTwo)
            {
                objectiveUIController.SetText("Get to the exit!");
                hasCodes = true;
            }
        }

    }

    public void HandlePickup(GameObject item)
    {
        switch(item.name)
        {
            case "RedKey":
                foundRedKey = true;
                break;
            case "GreenKey":
                foundGreenKey = true;
                break;
            case "Guitar":
                foundGuitar = true;
                break;
            case "Cup":
                foundCup = true;
                break;
            case "CodeOne":
                hasCodeOne = true;
                break;
            case "CodeTwo":
                hasCodeTwo = true;
                break;
        }

        AdvanceObjective();



    }
}
