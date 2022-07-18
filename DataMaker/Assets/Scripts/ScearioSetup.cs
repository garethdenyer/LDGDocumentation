using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScearioSetup : MonoBehaviour
{
    // Attached to Empty Holder

    public TMP_Dropdown solutionDropdown;
    public List<string> components = new List<string>(); //each individual component that needs its concentration/amount monitored
    public int colourcomponent;
    public float extinctioncoeff;

    void Awake()
    {
        SetUpComponents();
        SetUpSolutionsDropdown();
    }

    void SetUpComponents()
    {
        components.Add("Tris");  //component [0]
        components.Add("KCl"); //component [1]
        components.Add("Glucose"); //component [2]
        components.Add("Phenol Red"); //component [3]   Gives colour

        colourcomponent = 3;
        extinctioncoeff = 6.9f;
    }

    void SetUpSolutionsDropdown()
    {
        List<string> solutionOptions = new List<string>();
        solutionOptions.Add("Water");  //menu choice 0
        solutionOptions.Add("25 mM KCl"); //menu choice 1
        solutionOptions.Add("100 mM Tris Buffer"); //menu choice 2
        solutionOptions.Add("50 mM glucose"); //menu choice 3
        solutionOptions.Add("1 mM Phenol Red"); //menu choice 4

        solutionDropdown.ClearOptions();
        solutionDropdown.AddOptions(solutionOptions);
    }

    public Color UpdateColour(float concsubstance)
    {
        Color water = Color.white;
        Color substance = Color.red;
        Color blendedColour = Color.Lerp(water, substance, concsubstance);
        return blendedColour;
    }

    public void EnactDropdownChoice(int choice)  //triggered when dropdown interacted with
    {
        //firstly reset the list concentrations for every possible component so no inheritance from last choice
        for (int i = 0; i < components.Count; i++)
        {
            this.GetComponent<NewTube>().tempconcs[i] = 0f;
        }

        //now selectively change particular component concentrations according to choice
        switch (choice)
        {
            case 0: //water - no changes are needed
                this.GetComponent<NewTube>().chosensoln = "Water";
                break;
            case 1: //bromophenol is 1 mM
                this.GetComponent<NewTube>().tempconcs[1] = 25f;
                this.GetComponent<NewTube>().chosensoln = "25 mM KCl";
                break;
            case 2: //buffer Tris is 10 mM
                this.GetComponent<NewTube>().tempconcs[0] = 100f;
                this.GetComponent<NewTube>().chosensoln = "100 mM Tris";
                break;
            case 3: //NaCl is 500 mM
                this.GetComponent<NewTube>().tempconcs[2] = 50f;
                this.GetComponent<NewTube>().chosensoln = "50 mM glucose";
                break;
            case 4: //mixture of 5 buffer and 10 NaCl
                this.GetComponent<NewTube>().tempconcs[3] = 1f;
                this.GetComponent<NewTube>().chosensoln = "1 mM phenol red";
                break;
            default: //for when no choice registered (in case this happens right at the start)
                this.GetComponent<NewTube>().tempconcs[0] = 0f;
                this.GetComponent<NewTube>().chosensoln = "water";
                break;
        }
    }

    public void MakeSet()
    {
        //keep a note of how many tubes have already been made - used to set position of new tubes
        int prenotubes = this.GetComponent<NewTube>().tubes.Count;

        //for each define solution menu choice, volume, and label
        MakeSetTube(0, 2000f, "H20", prenotubes);
        MakeSetTube(1, 1000f, "KCl", prenotubes);
        MakeSetTube(2, 1000f, "Tris", prenotubes);
        MakeSetTube(3, 500f, "Glu", prenotubes);
        MakeSetTube(4, 50f, "PhRd", prenotubes);
    }

    public void MakeSetTube(int choice, float vol, string label, int alredytubes)
    {
        //want to have a reference to the tube it will be the last in the tubes list. Establish current length of list
        int currTubeCnt = this.GetComponent<NewTube>().tubes.Count;

        //set the required volume, choose correct solution from dropdown, create the tube
        this.GetComponent<NewTube>().volinput.text = vol.ToString();
        EnactDropdownChoice(choice);
        this.GetComponent<NewTube>().CreateTube();

        //the new tube will be the new last in the list - so since the list starts at zero can use previous count
        GameObject newtube = this.GetComponent<NewTube>().tubes[currTubeCnt];
        //label the tube and move it to the right
        newtube.GetComponentInChildren<TubeMiddleTrigger>().tubelabel.text = label;
        newtube.transform.position += new Vector3((currTubeCnt-alredytubes)*2, 0f, 1f);

        //reset the dropdown otherwise value above now default
        EnactDropdownChoice(0);
    }

}
