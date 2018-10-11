using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveUIController : MonoBehaviour {

    public string[] objectives;

    private int currentObjective = 0;

    public Text currObjectiveText;
    

	// Use this for initialization
	void Start () {
        currObjectiveText.text = objectives[currentObjective];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*public void UpdateUI(int currentObjective)
    {
        this.currentObjective = currentObjective;
        currObjectiveText.text = objectives[this.currentObjective];
    }*/

    public void SetText(string text)
    {
        currObjectiveText.text = text;
    }
}
