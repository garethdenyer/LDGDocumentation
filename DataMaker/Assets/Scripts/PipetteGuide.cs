using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipetteGuide : MonoBehaviour
{
    // Attached to Tube Top Trigger

    public bool tipengaged;

    private void OnMouseDown()
    {
        if(transform.parent.gameObject.GetComponent<TubeActions>().lidstatus != "Closed")
        {
            GameObject tip = GameObject.Find("Pipette"); //locate the pipette tip

            //set tipengaged in the previous activeTube to false and now clear the activeTube in PipetteAction 
            if (tip.GetComponent<PipetteAction>().activeTube != null) //not necessary to do if no activeTube
            {
                tip.GetComponent<PipetteAction>().activeTube.GetComponentInChildren<PipetteGuide>().tipengaged = false;  //tip no longer in tube
                tip.GetComponent<PipetteAction>().activeTube = null;
            }

            //establish a new activeTube in PipetteAction and set tipengaged in THIS tube to true
            tip.GetComponent<PipetteAction>().activeTube = transform.parent.gameObject;  //let the pipette tip know which tube it is above
            tipengaged = true;

            tip.GetComponent<PipetteAction>().SendTipToMeniscus();
        }
    }

}
