using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndController : MonoBehaviour {
    public List<GameObject> requiredObjects;

    private bool isComplete = false;

    public void CanComplete(Player player)
    {
        foreach (GameObject requiredObject in requiredObjects)
        {
            if (!player.HasItem(requiredObject))
            {
                isComplete = false;
                break;
            }
            isComplete = true;
        }

        if (isComplete)
        {
            gameObject.SetActive(false);
        }
    }
}
