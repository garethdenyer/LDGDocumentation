using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class PipetteAction : MonoBehaviour
{
    // Attached to pipette Tip

    public GameObject activeTube;  //set dynamically as pipette sent to a specific tube
    public TMP_InputField setVol;
    public GameObject thecanvas;

    public GameObject aliquot;
    public float aliquotvol;
    public List<float> aliquotconcs = new List<float>();

    public TMP_Text info;

    public string actionrecord;

    NewTube newtubescript;

    private void Start()
    {
        //make header row for action record 
        actionrecord = "Tube" + '\t' + "Action" + '\t' + "Volume" + '\n';
        aliquotvol = 0f;  //may be redundant
        newtubescript = FindObjectOfType<NewTube>();

        SetUpAliquotConcsList();
        AdjustAliquot();
    }

    public void SetUpAliquotConcsList()
    {
        //set up a holding list for aliquot concs
        for (int i = 0; i < newtubescript.components.Count; i++)
        {
            aliquotconcs.Add(0f);
        }
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

                    //quantities in aliquot before sucking up new amount
                    List<float> presuckaliquants = new List<float>();
                    for(int i=0; i < newtubescript.components.Count; i++)
                    {
                        presuckaliquants.Add(aliquotvol * aliquotconcs[i]);
                    }

                    //quantity sucked up from the tube
                    List<float> suckedupqties = new List<float>();
                    for (int i = 0; i < newtubescript.components.Count; i++)
                    {
                        suckedupqties.Add(uL * activeTube.GetComponentInChildren<LiquidInTube>().concs[i]);
                    }

                    //new quantities in the aliquot
                    List<float> postsuckaliquants = new List<float>();
                    for (int i = 0; i < newtubescript.components.Count; i++)
                    {
                        postsuckaliquants.Add(presuckaliquants[i] + suckedupqties[i]);
                    }

                    //change the parameters in the target tube (note both reductions)
                    for (int i = 0; i < newtubescript.components.Count; i++)
                    {
                        suckedupqties[i]=suckedupqties[i]*-1f;
                    }
                    activeTube.GetComponentInChildren<LiquidInTube>().AdjustVol(-uL, suckedupqties);

                    //change the volume and concentration of the aliquot
                    aliquotvol += uL;
                    for (int i = 0; i < newtubescript.components.Count; i++)
                    {
                        aliquotconcs[i] = postsuckaliquants[i] / aliquotvol;
                    }

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
                    List<float> qtstosend = new List<float>();
                    for (int i = 0; i < newtubescript.components.Count; i++)
                    {
                        qtstosend.Add(uL * aliquotconcs[i]);
                    }

                    //change the parameters in the target tube (both positives as we are adding to it)
                    activeTube.GetComponentInChildren<LiquidInTube>().AdjustVol(uL, qtstosend);

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
        Color blendedColour = Color.Lerp(water, substance, aliquotconcs[1]); //based on glucose - needs to be set centrally
        aliquot.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", blendedColour);
    }
}
