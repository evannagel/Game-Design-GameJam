using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIController : MonoBehaviour {

    public Image[] itemSprites;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateUI(List<GameObject> items)
    {
        foreach(GameObject item in items)
        {
            Image sprite = itemSprites[items.IndexOf(item)];

            Item itemScript = item.GetComponent<ItemController>().item;
            sprite.sprite = itemScript.itemSprite;

            Debug.Log(sprite.sprite + " - " + item.name);
        }
    }
}
