using UnityEngine;
using TMPro;

public class LiquidInTube : MonoBehaviour
{
    // Attached to Liquid part of a tube

    public float volul;
    public float conc;

    public GameObject liqcylinder;
    public float cylindercapacity;
    public float cylindermaxYheight;
    float cylinderdiam;
    float cylinderheight;

    public GameObject meniscus;
    public TMP_Text info;

    public GameObject pipettedialpin;


    private void Awake()
    {
        cylindercapacity = 2000f;  //defines capacity as 2 mL
        cylindermaxYheight = 1.6f;  //height of liqcylinder at cylindercapacity
        cylinderdiam = liqcylinder.transform.localScale.x;  //the diameter of the liquid cylinder in Unity units

        UpdateCylinderHeight();
        UpdateInfo();
    }


    public void AdjustVol(float volchange, float qtychange)
    {
        //calculate the quanity in the tube before the change
        float initqty = volul * conc;

        //check if change woudl make vol negative and adjust if necessary. Only applies on suckup.
        if (volul + volchange < 0) 
        {
            volchange = -volul;
            //need to adjust the quantity going out. It will now be calculated on volul, not volchange
            qtychange = conc * -volul;
        }

        //calcuate the new quantity in the tube
        float newqty = initqty + qtychange;

        //readjust the final volume and concentration
        volul += volchange;

        //deal with the case where calculation can't be calculated - perhaps only needed where volume is zero?
        if (float.IsNaN(newqty / volul) || volul == 0f)
        {
            conc = 0f;
        }
        else
        {
            conc = newqty / volul;
        }

        UpdateCylinderHeight();
        UpdateInfo();
    }

    void UpdateCylinderHeight()
    {
        cylinderheight = (volul / cylindercapacity) * cylindermaxYheight;  //the proportion of the tube that is full as function of tube height
        liqcylinder.transform.localScale = new Vector3(cylinderdiam, cylinderheight, cylinderdiam);
        liqcylinder.transform.localPosition = new Vector3(0f, cylinderheight, 0f);  //as the cylinder height increases, the position of the cylinder needs to go up.  Initialy it is at the bottom.
        
        float meniscusheight = cylinderheight*2f;  //meniscus is at top of cylinder - which is posn plus that again
        meniscus.transform.localPosition = new Vector3(0f, meniscusheight, 0f);  //move the meniscus marker
    }

    void UpdateInfo()
    {
        info.text = volul.ToString("N0") + '\n' + conc.ToString("N1") ;
    }
}
