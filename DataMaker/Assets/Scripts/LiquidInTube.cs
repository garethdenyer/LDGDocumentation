using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LiquidInTube : MonoBehaviour
{
    // Attached to Liquid part of a tube

    public float volul;
    public List<float> concs = new List<float>();

    public GameObject liqcylinder;
    public float cylindercapacity;
    public float cylindermaxYheight;
    float cylinderdiam;
    float cylinderheight;

    public GameObject meniscus;

    public GameObject pipettedialpin;

    ScearioSetup ScenarioSetupscript;

    private void Awake()
    {
        ScenarioSetupscript = FindObjectOfType<ScearioSetup>();
        cylindercapacity = 2000f;  //defines capacity as 2 mL
        cylindermaxYheight = 1.6f;  //height of liqcylinder at cylindercapacity
        cylinderdiam = liqcylinder.transform.localScale.x;  //the diameter of the liquid cylinder in Unity units

        for (int i = 0; i < ScenarioSetupscript.components.Count; i++)
        {
            concs.Add(0);
        }

        UpdateCylinderHeight();
    }


    public void AdjustVol(float volchange, List<float> qtychanges)
    {
        //calculate the quantity of each component in the tube before the change
        List<float> initialqtys = new List<float>();
        for (int i = 0; i < concs.Count; i++)
        {
            initialqtys.Add(volul * concs[i]);
        }

        //check if change woudl make vol negative and adjust if necessary. Only applies on suckup.
        if (volul + volchange < 0)
        {
            volchange = -volul;
            //need to adjust the quantity going out. It will now be calculated on volul, not volchange
            for (int i = 0; i < concs.Count; i++)
            {
                qtychanges[i] = (-volul * concs[i]);  //note the negative
            }
        }

        //calcuate the new quantity in the tube
        List<float> newqtys = new List<float>();
        for (int i = 0; i < concs.Count; i++)
        {
            newqtys.Add(initialqtys[i] + qtychanges[i]);
        }

        //readjust the final volume and concentration
        volul += volchange;

        //deal with the case where calculation can't be calculated - perhaps only needed where volume is zero?
        for (int i = 0; i < concs.Count; i++)
        {
            if (float.IsNaN(newqtys[i] / volul) || volul == 0f)
            {
                concs[i] = 0f;
            }
            else
            {
                concs[i] = newqtys[i] / volul;
            }
        }

        UpdateCylinderHeight();
        //Update colour
        liqcylinder.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", ScenarioSetupscript.UpdateColour(concs[ScenarioSetupscript.colourcomponent]));
    }

    void UpdateCylinderHeight()
    {
        cylinderheight = (volul / cylindercapacity) * cylindermaxYheight;  //the proportion of the tube that is full as function of tube height
        liqcylinder.transform.localScale = new Vector3(cylinderdiam, cylinderheight, cylinderdiam);
        liqcylinder.transform.localPosition = new Vector3(0f, cylinderheight, 0f);  //as the cylinder height increases, the position of the cylinder needs to go up.  Initialy it is at the bottom.

        float meniscusheight = cylinderheight * 2f;  //meniscus is at top of cylinder - which is posn plus that again
        meniscus.transform.localPosition = new Vector3(0f, meniscusheight, 0f);  //move the meniscus marker
    }
}
