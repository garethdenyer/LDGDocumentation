using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScearioSetup : MonoBehaviour
{
    // Attached to Empty Holder

    public TMP_Dropdown solutionDropdown;
    public List<string> components = new List<string>(); //each individual component that needs its concentration/amount monitored


    void Awake()
    {
        SetUpComponents();
        SetUpSolutionsDropdown();
    }

    void SetUpComponents()
    {
        components.Add("Tris");  //component [0]
        components.Add("BromoBlue"); //component [1]
        components.Add("NaCl"); //component [2]
    }

    void SetUpSolutionsDropdown()
    {
        List<string> solutionOptions = new List<string>();
        solutionOptions.Add("Water");
        solutionOptions.Add("1 mM Bromophenol Blue");
        solutionOptions.Add("10 mM Tris Buffer");
        solutionOptions.Add("500 mM NaCl");
        solutionOptions.Add("5 mM Tris/10 mM NaCl");

        solutionDropdown.ClearOptions();
        solutionDropdown.AddOptions(solutionOptions);
    }

    public Color UpdateColour(float concsubstance)
    {
        Color water = Color.white;
        Color substance = Color.blue;
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
                this.GetComponent<NewTube>().tempconcs[1] = 1f;
                this.GetComponent<NewTube>().chosensoln = "1 mM BromoBlue";
                break;
            case 2: //buffer Tris is 10 mM
                this.GetComponent<NewTube>().tempconcs[0] = 10f;
                this.GetComponent<NewTube>().chosensoln = "10 mM Tris";
                break;
            case 3: //NaCl is 500 mM
                this.GetComponent<NewTube>().tempconcs[2] = 500f;
                this.GetComponent<NewTube>().chosensoln = "500 mM NaCl";
                break;
            case 4: //mixture of 5 buffer and 10 NaCl
                this.GetComponent<NewTube>().tempconcs[2] = 10f;
                this.GetComponent<NewTube>().tempconcs[0] = 5f;
                this.GetComponent<NewTube>().chosensoln = "5 mM Tris/10 mM NaCl";
                break;
            default: //for when no choice registered (in case this happens right at the start)
                this.GetComponent<NewTube>().tempconcs[0] = 0f;
                this.GetComponent<NewTube>().chosensoln = "water";
                break;
        }
    }

}
