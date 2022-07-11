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
    public TMP_Text info;

    private void Start()
    {
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
                    if (uL > activeTube.GetComponentInChildren<LiquidInTube>().volul)
                    {
                        uL = activeTube.GetComponentInChildren<LiquidInTube>().volul;
                    }
                    activeTube.GetComponentInChildren<LiquidInTube>().AdjustVol(-uL);
                    aliquotvol += uL;
                }

                else if (direction == "dispense")
                {
                    if (uL >= aliquotvol)
                    {
                        uL = aliquotvol;
                    }
                        activeTube.GetComponentInChildren<LiquidInTube>().AdjustVol(uL);
                        aliquotvol -= uL;
                }

                AdjustAliquot();
                SendTipToMeniscus();
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


}
