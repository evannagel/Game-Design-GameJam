using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    private float smooth = 2.0f;
    private float DoorOpenAngle = 90.0f;
    private float DoorCloseAngle = 0.0f;
    private bool open = false;
    
    //Main function
    void Update()
    {
        if (open == true)
        {
            Quaternion target = Quaternion.Euler(0, DoorOpenAngle, 0);
            // Dampen towards the target rotation
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target,
            Time.deltaTime * smooth);
        }

    if (open == false)
        {
            Quaternion target1 = Quaternion.Euler(0, DoorCloseAngle, 0);
            // Dampen towards the target rotation
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target1,
            Time.deltaTime * smooth);
        }
    }

    public void OpenDoor()
    {
        open = !open;
    }
}
