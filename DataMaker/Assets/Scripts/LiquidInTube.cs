using UnityEngine;

public class LiquidInTube : MonoBehaviour
{
    // Attached to Liquid part of a tube

    public float volul;
    public GameObject cylinder;
    public GameObject plastic;
    public float cylindercapacity;

    public GameObject dropletPrefab;

    private void Start()
    {
        cylindercapacity = 1000f;
    }


    public void AdjustVol(float volchange)
    {
        if (volul + volchange < 0) //if change woudl make vol negative
        {
            volchange = -volul;
        }

        volul += volchange;
        UpdateCylinderHeight();
    }

    void UpdateCylinderHeight()
    {
        float cylinderdiam = cylinder.transform.localScale.x;  //the diameter of the liquid cylinder in Unity units
        float tubemaxYheight = plastic.transform.localScale.y;  //the height of the tube in Unity units
        float cylinderheight = (volul / cylindercapacity) * tubemaxYheight;  //the proportion of the tube that is full as function of tube height
        cylinder.transform.localScale = new Vector3(cylinderdiam, cylinderheight, cylinderdiam);
        cylinder.transform.localPosition = new Vector3(0f, cylinderheight - tubemaxYheight, 0f);  //as the cylinder height increases, the position of the cylinder needs to go up.  Initialy it is at the bottom.
    }


    public void Remove100()
    {
        AdjustVol(-100f);
    }

    public void Add100()
    {
        AdjustVol(100f);
    }

}
