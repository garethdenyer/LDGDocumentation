using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanning : MonoBehaviour
{
    float scanspeed = 0.5f;
    float zoomSpeed = 4.0f;
    Vector3 target = new Vector3(0, 0, 0);
    float updown;
    float leftright;
    float scroll;

    // Update is called once per frame
    void Update()
    {
        updown = Input.GetAxis("Vertical") * scanspeed;
        transform.RotateAround(target, Vector3.left, -updown);

        leftright = Input.GetAxis("Horizontal") * scanspeed;
        //transform.RotateAround(target, Vector3.up, -leftright);
        transform.Translate(leftright, 0, 0);

        scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(0, 0, scroll * zoomSpeed);
    }
}
