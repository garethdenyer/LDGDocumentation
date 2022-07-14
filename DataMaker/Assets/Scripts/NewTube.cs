using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewTube : MonoBehaviour
{
    // Attached to EmptyHolder in Scene

    public GameObject tubePrefab;
    public TMP_InputField volume;

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

        if (float.TryParse(volume.text, out float uL))
        {
            if (uL > newtube.GetComponentInChildren<LiquidInTube>().cylindercapacity)
            {
                uL = newtube.GetComponentInChildren<LiquidInTube>().cylindercapacity;
            }
            newtube.GetComponentInChildren<LiquidInTube>().AdjustVol(uL);
        }

        newtube.GetComponent<TubeActions>().tubelabel.text = tubeNo.ToString();
    }
}
