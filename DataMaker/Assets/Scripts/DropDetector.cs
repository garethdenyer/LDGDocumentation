using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDetector : MonoBehaviour
{
    // Attached to Droplet Detector within the Liquid part of a tube

    public GameObject liquid;

    private void OnTriggerEnter(Collider other)
    {
        liquid.GetComponent<LiquidInTube>().Add100();  //when droplet detector triggered, run the add function
        Destroy(other.gameObject);
    }
}
