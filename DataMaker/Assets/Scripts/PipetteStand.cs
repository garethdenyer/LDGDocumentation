using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipetteStand : MonoBehaviour
{
    // Attached to PipetteStand

    public GameObject pipettePrefab;

    private void OnMouseDown()
    {
        if (GameObject.Find("Pipette"))
        {
            GameObject tip = GameObject.Find("Pipette"); //locate the pipette tip

            //set tipengaged in the previous activeTube to false and now clear the activeTube in PipetteAction 
            if (tip.GetComponent<PipetteAction>().activeTube != null) //not necessary to do if no activeTube
            {
                tip.GetComponent<PipetteAction>().activeTube.GetComponentInChildren<PipetteGuide>().tipengaged = false;  //tip no longer in tube
                tip.GetComponent<PipetteAction>().activeTube = null;
            }

            tip.transform.position = transform.position + new Vector3(0f, 2.7f, 0f);  //places pipette just just on stand
        }

        else  //create the pipette
        {
            GameObject pipette = Instantiate(pipettePrefab, transform.position + new Vector3(0f, 2.7f, 0f), transform.rotation);
            pipette.name = "Pipette";
        }

    }
}
