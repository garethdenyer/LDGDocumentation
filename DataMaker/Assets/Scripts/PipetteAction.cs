using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipetteAction : MonoBehaviour
{
    // Attached to pipette Tip

    public GameObject dropletPrefab;
    public GameObject dropletPoint;
    public GameObject activeTube;
    
    public void SuckUp()
    {
        activeTube.GetComponentInChildren<LiquidInTube>().Remove100();
    }

    public void Dispense()
    {
        GameObject droplet = Instantiate(dropletPrefab, dropletPoint.transform.position, transform.rotation);
    }
}
