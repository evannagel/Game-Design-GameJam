using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIController : MonoBehaviour {

    public Image[] itemSprites;

	// Use this for initialization
	void Start () {
        ResetImages();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetImages()
    {
        foreach(Image image in itemSprites)
        {
            image.enabled = false;
        }
    }

    public void UpdateUI(List<GameObject> items)
    {
        ResetImages();
        foreach(GameObject item in items)
        {
            Image sprite = itemSprites[items.IndexOf(item)];
            sprite.enabled = true;

            Item itemScript = item.GetComponent<ItemController>().item;
            sprite.sprite = itemScript.itemSprite;
        }
    }
}
