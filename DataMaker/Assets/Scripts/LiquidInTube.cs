using UnityEngine;
using TMPro;

public class LiquidInTube : MonoBehaviour
{
    // Attached to Liquid part of a tube

    public float volul;
    public GameObject liqcylinder;
    public GameObject model;
    public float cylindercapacity;

    public GameObject meniscus;
    public TMP_Text info;


    private void Start()
    {
        cylindercapacity = 1000f;

        UpdateCylinderHeight();
        UpdateInfo();
    }


    public void AdjustVol(float volchange)
    {
        if (volul + volchange < 0) //if change woudl make vol negative
        {
            volchange = -volul;
        }

        volul += volchange;

        UpdateCylinderHeight();
        UpdateInfo();
    }

    void UpdateCylinderHeight()
    {
        float cylinderdiam = liqcylinder.transform.localScale.x;  //the diameter of the liquid cylinder in Unity units
        float tubemaxYheight = model.transform.localScale.y;  //the height of the tube in Unity units
        float cylinderheight = (volul / cylindercapacity) * tubemaxYheight;  //the proportion of the tube that is full as function of tube height
        liqcylinder.transform.localScale = new Vector3(cylinderdiam, cylinderheight, cylinderdiam);
        liqcylinder.transform.localPosition = new Vector3(0f, cylinderheight - tubemaxYheight, 0f);  //as the cylinder height increases, the position of the cylinder needs to go up.  Initialy it is at the bottom.
        
        float meniscusheight = liqcylinder.transform.localPosition.y + cylinderheight;  //track the top of the liquid
        meniscus.transform.localPosition = new Vector3(0f, meniscusheight, 0f);  //move the meniscus marker
    }

    void UpdateInfo()
    {
        info.text = volul.ToString("N0");
    }
}
