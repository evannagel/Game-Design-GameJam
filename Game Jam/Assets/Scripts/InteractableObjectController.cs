using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObjectController : MonoBehaviour {

    public Canvas actionCanvas;

    private Camera cam;
    private GameObject interactedObject;
    private Player player;


    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        player = GetComponent<Player>();
        actionCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForRaycastHit();
    }

    private void CheckForRaycastHit()
    {
        int groupedLayerMasks = LayerMask.GetMask("Item", "DoorOpen", "KeyUse");

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, 5f, groupedLayerMasks))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            string layerName = LayerMask.LayerToName(hit.transform.gameObject.layer);
            Sprite actionSprite = GetActionSprite(layerName);

            interactedObject = hit.transform.gameObject;

            switch (layerName)
            {
                case "KeyUse":
                    ActivateCanvas(actionSprite);
                    break;
                case "Item":
                    Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                    CheckForPickup();
                    ActivateCanvas(actionSprite);
                    break;
                case "DoorOpen":
                    Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                    OpenDoor(interactedObject);
                    ActivateCanvas(actionSprite);
                    break;
                default:
                    break;
            }
        }
        else
        {
            interactedObject = null;
            actionCanvas.enabled = false;
        }
    }

    private void ActivateCanvas( Sprite sprite )
    {
        actionCanvas.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        actionCanvas.enabled = true;
    }

    private void CheckForPickup()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUpItem();
        }
    }

    private bool PressesActionKey()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            return true;
        }

        return false;
    }

    private void CheckForClickedObject(GameObject go)
    {

    }

    private void OpenDoor(GameObject doorObject)
    {
        if (PressesActionKey())
        {
            DoorController Door = doorObject.GetComponent<DoorController>();
            Door.OpenDoor();
        }
    }

    private Sprite GetActionSprite(string layerName)
    {
        string spriteName = "";
        switch (layerName)
        {
            case "Item":
                spriteName = "pickup";
                break;
            case "KeyUse":
                Debug.Log("Hovering over Key");
                spriteName = "key";
                break;
            case "DoorOpen":
                Debug.Log("Hovering over Door");
                spriteName = "door";
                break;
        }

        return Resources.Load<Sprite>("Sprites/" + spriteName); ;
    }

    void PickUpItem()
    {
        player.AddItem(interactedObject);
    }

    public void ClearValues()
    {
        interactedObject = null;
        actionCanvas.enabled = false;
    }
}
