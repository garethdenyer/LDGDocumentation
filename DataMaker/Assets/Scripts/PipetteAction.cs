using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PipetteAction : MonoBehaviour
{
    // Attached to pipette Tip

    public GameObject activeTube;  //set dynamically as pipette sent to a specific tube
    public TMP_InputField setVol;
    public GameObject thecanvas;
    
    public void ThumbAction(string direction)
    {
        if(activeTube != null)
        {
            if (float.TryParse(setVol.text, out float uL))
            {
                if(direction == "suckup")  //there may be other options!
                {
                    activeTube.GetComponentInChildren<LiquidInTube>().AdjustVol(-uL);
                }
                else
                {
                    activeTube.GetComponentInChildren<LiquidInTube>().AdjustVol(uL);
                }

                SendTipToMeniscus();  
            }
        }
    }



    public void SendTipToMeniscus()
    {
        float pipmenoffset = 2.1f; //distance to maintain between mensiscus and middle of pipette
        Vector3 meniscusposn = activeTube.GetComponentInChildren<LiquidInTube>().meniscus.transform.position;
        Vector3 topoftube = activeTube.GetComponentInChildren<LiquidInTube>().pipettedialpin.transform.position;

        transform.position = meniscusposn + new Vector3(0f, pipmenoffset, 0f);

        thecanvas.transform.position =  topoftube;
    }


}
