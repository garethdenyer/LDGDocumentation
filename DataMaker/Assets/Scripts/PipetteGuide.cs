using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipetteGuide : MonoBehaviour
{
    // Attached to Tube

    public GameObject plastic;

    public void DirectTipToTube()
    {

        GameObject tip = GameObject.Find("PipetteTip"); //locate the pipette tip
        tip.transform.position = plastic.transform.position + new Vector3(0f, 4f, 0f);  //send it to above the tube

        tip.GetComponent<PipetteAction>().activeTube = gameObject;  //let the pipette tip know which tube it is above

    }

}
