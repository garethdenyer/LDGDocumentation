using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PipetteAction : MonoBehaviour
{
    // Attached to pipette Tip

    public GameObject activeTube;
    public TMP_InputField setVol;
    
    public void SuckUp()
    {
        if(activeTube != null)
        {
            if (float.TryParse(setVol.text, out float uL))
            {
                activeTube.GetComponentInChildren<LiquidInTube>().AdjustVol(-uL);
                SendTipToMeniscus();  
            }
        }
    }

    public void Dispense()
    {
        if (activeTube != null)
        {
            if (float.TryParse(setVol.text, out float uL))
            {
                activeTube.GetComponentInChildren<LiquidInTube>().AdjustVol(uL);
                SendTipToMeniscus();
            }
        }
    }


    public void SendTipToMeniscus()
    {
        transform.position = 
            activeTube.GetComponentInChildren<LiquidInTube>().meniscus.transform.position 
            + new Vector3(0f, 1f, 0f);
    }


}
