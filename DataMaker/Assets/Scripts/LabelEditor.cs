using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LabelEditor : MonoBehaviour
{
    // On Empty Holder

    public GameObject EditPanel;
    public TMP_InputField labeltext;
    public TMP_Text editortitle;
    public TMP_Text editorInfo;
    GameObject activeObject;
    string objectType;

    public void ShowPanel(GameObject origin, string type, string currtext, Vector3 place, string title, string info)
    {
        EditPanel.SetActive(true);
        EditPanel.transform.position = place;
        labeltext.text = currtext;
        activeObject = origin;
        objectType = type;
        editortitle.text = title;
        editorInfo.text = info;
    }

    public void SetLabelText()
    {
        if(objectType == "Tube")
        {
            activeObject.GetComponentInChildren<TubeMiddleTrigger>().ProcessLableEdit(labeltext.text);
        }

        EditPanel.SetActive(false);
    }
}
