using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewTube : MonoBehaviour
{
    // Attached to EmptyHolder in Scene

    public GameObject tubePrefab;
    public TMP_InputField volinput;

    int tubeNo;
    public List<GameObject> tubes = new List<GameObject>();
    public List<float> tempconcs = new List<float>();

    public string chosensoln;
    public string creationrecord;

    ScearioSetup ScenarioSetupScript;

    private void Start()
    {
        ScenarioSetupScript = FindObjectOfType<ScearioSetup>();
        tubeNo = 0;
        SetUpTempConcsList();
        ScenarioSetupScript.EnactDropdownChoice(0);
    }

    void SetUpTempConcsList()
    {
        for (int i = 0; i < ScenarioSetupScript.components.Count; i++)
        {
            tempconcs.Add(0f);
        }
    }

    public void CreateTube()
    {
        tubeNo += 1;
        GameObject newtube = Instantiate(tubePrefab, new Vector3(0f, 0f, 0f), transform.rotation);
        tubes.Add(newtube);

        newtube.transform.name = "T" + tubeNo.ToString();
        newtube.GetComponent<TubeActions>().tubelabel.text = tubeNo.ToString();

        //Set up variables to work out volume and amount to add.
        float vol=0;
        List<float> qtys = new List<float>();

        //decide on the volume.  It mustn't exceed the tube capacity
        if (float.TryParse(volinput.text, out float uL))
        {
            if (uL > newtube.GetComponentInChildren<LiquidInTube>().cylindercapacity)
            {
                vol = newtube.GetComponentInChildren<LiquidInTube>().cylindercapacity;
            }
            else
            {
                vol = uL;
            }
        }

        //decide on the quantities - these will be derived from the concentrations set in the dropdown
        for (int i = 0; i < tempconcs.Count; i++)
        {
            qtys.Add(tempconcs[i] * vol);
        }

        //send that volume and quantity to the tube 
        newtube.GetComponentInChildren<LiquidInTube>().AdjustVol(vol, qtys);

        //add to creation record
        creationrecord += newtube.transform.name + '\t' + vol.ToString("N0") + " uL" + '\t' + chosensoln + '\t' + (Mathf.Round(Time.time) / 60f).ToString("N2") + " m" + '\n';
    }
}
