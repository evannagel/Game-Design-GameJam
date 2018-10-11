using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupItemController : MonoBehaviour {

    public bool showPickupSign = false;
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
        int groupedLayerMasks = LayerMask.GetMask("Item");

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, 2f, groupedLayerMasks))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);


            string layerName = LayerMask.LayerToName(hit.transform.gameObject.layer);
            Sprite actionSprite = GetActionSprite(layerName);

            switch (layerName)
            {
                case "Item":
                    Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                    showPickupSign = true;
                    interactedObject = hit.transform.gameObject;
                    CheckForPickup();
                    actionCanvas.transform.GetChild(0).GetComponent<Image>().sprite = actionSprite;
                    actionCanvas.enabled = true;
                    break;
                default:
                    break;
            }







        }
        else
        {
            showPickupSign = false;
            interactedObject = null;
            actionCanvas.enabled = false;
        }
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


    private Sprite GetActionSprite(string layerName)
    {
        string spriteName = "";
        switch (layerName)
        {
            case "Item":
                spriteName = "pickup";
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
        showPickupSign = false;
        interactedObject = null;
        actionCanvas.enabled = false;
    }
}
