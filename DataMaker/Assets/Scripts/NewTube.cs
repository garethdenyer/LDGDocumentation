using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewTube : MonoBehaviour
{
    // Attached to EmptyHolder in Scene

    public GameObject tubePrefab;
    public TMP_InputField volinput;
    public TMP_InputField concinput;
    int tubeNo;

    private void Start()
    {
        tubeNo = 0;
    }

    public void CreateTube()
    {
        tubeNo += 1;
        GameObject newtube = Instantiate(tubePrefab, new Vector3(0f, 0f, 0f), transform.rotation);

        newtube.transform.name = "T" + tubeNo.ToString();
        newtube.GetComponent<TubeActions>().tubelabel.text = tubeNo.ToString();

        //Set up variables to work out volume and amount to add.
        float vol=0;
        float qty=0;

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

        //decide on the concentration
        if (float.TryParse(concinput.text, out float conc)) 
        {
            qty = conc * vol;
        }

        //send that volume and quantity to the tube 
        newtube.GetComponentInChildren<LiquidInTube>().AdjustVol(vol, qty);
    }
}
