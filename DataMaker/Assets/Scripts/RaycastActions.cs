using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastActions : MonoBehaviour
{
    // Attached to Main Camera

    Camera mainCamera;
    RaycastHit hit;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 worldMousePosition = 
            mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
        Vector3 direction = worldMousePosition - mainCamera.transform.position;
        Debug.DrawRay(mainCamera.transform.position, direction * 25, Color.green);

        //when the mouse buttton is pressed
        if (Input.GetMouseButtonDown(0))
        {
            //The line of code below is AMAZING! It stops the raycast from going through UI elements...
            //so very VERY useful to know. It should always be placed within the mouse IF statements.
            if (EventSystem.current.IsPointerOverGameObject()) return;

            //check what the ray has hit - it is only colliders that get registered
            if (Physics.Raycast(mainCamera.transform.position, direction, out hit, 100f))
            {
                if (hit.transform.name == "TopTrigger")
                {
                    hit.transform.gameObject.GetComponent<PipetteGuide>().DirectPipette();
                }
            }
        }

        //when the right mouse buttton is pressed
        if (Input.GetMouseButtonDown(1))
        {
            //Stops the raycast from going through UI elements (as described above)...
            if (EventSystem.current.IsPointerOverGameObject()) return;

            //check what the ray has hit - it is only colliders that get registered
            if (Physics.Raycast(mainCamera.transform.position, direction, out hit, 100f))
            {
                if (hit.transform.name == "TopTrigger")
                {
                    hit.transform.parent.gameObject.GetComponent<TubeActions>().LidActions();
                }
                if (hit.transform.name == "LidTrigger")
                {
                    //the lid trigger is many levels removed from where the TubeActions script is
                    GameObject tubetoplevel = hit.transform.parent.parent.parent.parent.gameObject;
                    tubetoplevel.GetComponent<TubeActions>().LidActions();
                }
            }
        }
    }
}
