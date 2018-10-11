using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeController : MonoBehaviour {
    public GameObject offeredObject;
    public GameObject requestedObject;

    public void TradeWith(Player player)
    {
        if (player.HasItem(requestedObject))
        {
            player.RemoveItem(requestedObject);
            player.AddItem(offeredObject);
        }
    }
}
