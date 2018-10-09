using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameraRotator : MonoBehaviour {


    public float rotateSpeed = 1f;



    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, (Mathf.Sin(Time.realtimeSinceStartup) * rotateSpeed) + transform.eulerAngles.y, transform.eulerAngles.z);

          

    }
}
