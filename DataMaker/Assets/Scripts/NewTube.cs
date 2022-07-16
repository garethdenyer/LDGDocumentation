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

    public TMP_Dropdown solutionDropdown;
    public List<string> components = new List<string>(); //each individual component that needs its concentration/amount monitored
    List<float> temporaryconcs = new List<float>();  //needs to be on this level as the list has to be accesed between functions

    string chosensoln;
    public string creationrecord;

    private void Start()
    {
        tubeNo = 0;

        SetUpComponents(); 
        SetUpTempConcsList();
        SetUpSolutionsDropdown();
        EnactDropdownChoice(0); //to ensure there is no funny business from making a tube without first using dropdown

        //force pipette tip to create list of relevant concentrations
        //not necessary if pipette is instantiated later
        GameObject tip = GameObject.Find("Pipette"); //locate the pipette tip
        tip.GetComponent<PipetteAction>().SetUpAliquotConcsList();
    }

    void SetUpComponents()
    {
        components.Add("Tris");
        components.Add("Phenol Red");
    }

    void SetUpTempConcsList()
    {
        for (int i = 0; i < components.Count; i++)
        {
            temporaryconcs.Add(0f);
        }
    }

    void SetUpSolutionsDropdown()
    {
        List<string> solutionOptions = new List<string>();
        solutionOptions.Add("Water");
        solutionOptions.Add("1 mM Phenol Red");
        solutionOptions.Add("Tris Buffer");

        solutionDropdown.ClearOptions();
        solutionDropdown.AddOptions(solutionOptions);
    }

    public void EnactDropdownChoice(int choice)  //triggered when dropdown interacted with
    {
        //firstly reset the list concentrations for every possible component so no inheritance from last choice
        for (int i = 0; i < components.Count; i++)
        {
            temporaryconcs[i] = 0f;
        }

        //now selectively change particular component concentrations according to choice
        switch (choice)
        {
            case 0: //water - no changes are needed
                chosensoln = "Water";
                break;
            case 1: //glucose is 1 mM
                temporaryconcs[1] = 1f;
                chosensoln = "1 mM Phenol Red";
                break;            
            case 2: //buffer Tris is 10 mM
                temporaryconcs[0] = 10f;
                chosensoln = "10 mM Tris";
                break;
            default: //for when no choice registered (in case this happens right at the start)
                temporaryconcs[0] = 0f;
                chosensoln = "water";
                break;
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
        for (int i = 0; i < components.Count; i++)
        {
            qtys.Add(temporaryconcs[i] * vol);
        }

        //send that volume and quantity to the tube 
        newtube.GetComponentInChildren<LiquidInTube>().AdjustVol(vol, qtys);

        //add to creation record
        creationrecord += newtube.transform.name + '\t' + vol.ToString("N0") + " uL" + '\t' + chosensoln + '\n';
    }
}
