using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipetteGuide : MonoBehaviour
{
    // Attached to Tube

    public void DirectTipToTube()
    {
        GameObject tip = GameObject.Find("Pipette"); //locate the pipette tip
        tip.GetComponent<PipetteAction>().activeTube = gameObject;  //let the pipette tip know which tube it is above

        tip.GetComponent<PipetteAction>().SendTipToMeniscus();
    }

}
