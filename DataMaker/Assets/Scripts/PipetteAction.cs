using TMPro;
using UnityEngine;

public class PipetteAction : MonoBehaviour
{
    // Attached to pipette Tip

    public GameObject activeTube;  //set dynamically as pipette sent to a specific tube
    public TMP_InputField setVol;
    public GameObject thecanvas;

    public GameObject aliquot;
    public float aliquotvol;
    public float aliquotconc;
    public TMP_Text info;

    public string actionrecord;

    private void Start()
    {
        //make header row for action record 
        actionrecord = "Tube" + '\t' + "Action" + '\t' + "Volume" + '\n';

        AdjustAliquot();
    }

    public void ThumbAction(string direction)
    {
        if (activeTube != null)
        {
            if (float.TryParse(setVol.text, out float uL))
            {
                if (direction == "suckup")  //there may be other options!
                {
                    //check that volume is not greater than what is in tube
                    if (uL > activeTube.GetComponentInChildren<LiquidInTube>().volul)
                    {
                        uL = activeTube.GetComponentInChildren<LiquidInTube>().volul;
                    }

                    //quantity in aliquot before sucking up new amount
                    float aliquotpresuckupqty = aliquotvol * aliquotconc;
                    //quantity sucked up from the tube
                    float suckupqty = uL * activeTube.GetComponentInChildren<LiquidInTube>().conc;
                    //new quantity in the aliquot
                    float postsuckupqty = aliquotpresuckupqty + suckupqty;

                    //change the parameters in the target tube (note both reductions)
                    activeTube.GetComponentInChildren<LiquidInTube>().AdjustVol(-uL, -suckupqty);

                    //change the volume and concentration of the aliquot
                    aliquotvol += uL;
                    aliquotconc = postsuckupqty / aliquotvol;

                    //record action
                    actionrecord += activeTube.name + '\t' + "Suck up" + '\t' + uL + '\n';
                }

                else if (direction == "dispense")
                {
                    //check that the volume is not greater than what is in the pipette aliquot
                    if (uL >= aliquotvol)
                    {
                        uL = aliquotvol;
                    }

                    //calcuate the quantity to send to the target tube
                    float aliquotqty = uL * aliquotconc;

                    //change the parameters in the target tube (both positives as we are adding to it)
                    activeTube.GetComponentInChildren<LiquidInTube>().AdjustVol(uL, aliquotqty);

                    //Change the volume of the aliquot - note that concentration does NOT change
                    aliquotvol -= uL;

                    //record action
                    actionrecord += activeTube.name + '\t' + "Dispense" + '\t' + uL + '\n';
                }

                AdjustAliquot();
                SendTipToMeniscus();
                UpdateColour();
            }
        }
    }

    void AdjustAliquot()
    {
        //adjust size and position of aliquot
        float aliheight = aliquotvol * 2f / 200f;
        aliquot.transform.localScale = new Vector3(0.3f, aliheight, 0.3f);
        aliquot.transform.localPosition = new Vector3(0f, aliheight - 2f, 0f);

        //report the volume in the aliquot
        info.text = aliquotvol.ToString("N0");
    }

    public void SendTipToMeniscus()
    {
        float pipmenoffset = 2.1f; //distance to maintain between mensiscus and middle of pipette
        Vector3 meniscusposn = activeTube.GetComponentInChildren<LiquidInTube>().meniscus.transform.position;
        Vector3 topoftube = activeTube.GetComponentInChildren<LiquidInTube>().pipettedialpin.transform.position;

        transform.position = meniscusposn + new Vector3(0f, pipmenoffset, 0f);

        thecanvas.transform.position = topoftube + new Vector3(0f, 0f, -1f);  //the minus one keeps the canvas in front of the pipette
    }

    void UpdateColour()
    {
        Color water = Color.white;
        Color substance = Color.red;
        Color blendedColour = Color.Lerp(water, substance, aliquotconc);
        aliquot.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", blendedColour);
    }
}
