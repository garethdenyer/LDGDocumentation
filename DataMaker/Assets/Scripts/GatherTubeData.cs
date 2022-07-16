using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GatherTubeData : MonoBehaviour
{
    // On Empty Holder

    string spreadsheet; //the data in a form for copy to XL
    public TMP_InputField datapresentation;  //an input field to which data can be passed for copying in play mode
    public GameObject datadisplaypanel;  //a panel that contains the input field.  Can be made visible/hidden.

    public void GetAbsorbances()
    {
        //make header row - note completelynew spreadsheet each time this is called
        spreadsheet = "Absorbance Readings" + '\n' + '\n' + "Tube" + '\t' + "Absorbance" + '\n';  

        //scroll through each tube, calcuate each absorbance, create each line item
        foreach (GameObject tub in this.GetComponent<NewTube>().tubes)
        {
            float absorbance = tub.GetComponentInChildren<LiquidInTube>().concs[1] * 6.9f; //using extinction coefficient of 6.9...
            spreadsheet += tub.transform.name + '\t' + absorbance.ToString("N3") + '\n';
        }

        //send the completed spreadsheet to an input field so it can be copied
        datapresentation.text = spreadsheet;
        //display the dialog containing that input field
        datadisplaypanel.SetActive(true);
    }

    public void GetPipetteActions()
    {
        GameObject tip = GameObject.Find("Pipette"); //locate the pipette tip

        //send the completed spreadsheet to an input field so it can be copied
        datapresentation.text = tip.GetComponent<PipetteAction>().actionrecord;
        //display the dialog containing that input field
        datadisplaypanel.SetActive(true);
    }

    public void GetTubeCreations()
    {
        string tubeendings = "Endpoints" + '\n';

        //scroll through each tube, obtain each concentration
        foreach (GameObject tub in this.GetComponent<NewTube>().tubes)
        {
            tubeendings += tub.transform.name + '\n';
            for (int i=0; i<this.GetComponent<NewTube>().components.Count; i++)
            {
                tubeendings += this.GetComponent<NewTube>().components[i] + '\t' + tub.GetComponentInChildren<LiquidInTube>().concs[i].ToString("N2")+'\n';
            }
        }

        //send the completed spreadsheet to an input field so it can be copied, adding tubeendings
        datapresentation.text = this.GetComponent<NewTube>().creationrecord + '\n' + tubeendings;
        //display the dialog containing that input field
        datadisplaypanel.SetActive(true);
    }
}