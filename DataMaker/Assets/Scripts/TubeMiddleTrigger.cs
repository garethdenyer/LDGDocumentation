using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TubeMiddleTrigger : MonoBehaviour
{
    // Attached to Middle Trigger on Tube

    public TMP_Text tubelabel;
    LabelEditor labeleditorscript;

    private void Start()
    {
        labeleditorscript = FindObjectOfType<LabelEditor>();
    }

    public void EditTubeLabel()
    {
        Vector3 mouseposn = Input.mousePosition;
        GameObject tube= transform.parent.gameObject;
        string volume = tube.GetComponentInChildren<LiquidInTube>().volul.ToString("N0") + " uL";
        labeleditorscript.ShowPanel(tube, "Tube", tubelabel.text, mouseposn, tube.transform.name, volume);
    }

    public void ProcessLableEdit(string newtext)
    {
        tubelabel.text = newtext;
    }
}
