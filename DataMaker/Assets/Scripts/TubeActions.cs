using UnityEngine;
using TMPro;

public class TubeActions : MonoBehaviour
{
    // Attached to Tube

    Animator lidcontroller;
    public string lidstatus;
    public GameObject pipetteguidebutton;

    public TMP_Text tubelabel;

    private void Start()
    {
        lidstatus = "Open";
        lidcontroller = GetComponent<Animator>();
        LidActions();
    }

    public void LidActions()
    {
        bool tipintube = gameObject.GetComponent<PipetteGuide>().tipengaged;

        if (lidstatus == "AlreadyOpen" && tipintube==false)
        {
            lidcontroller.SetBool("CloseAction", true);
            lidcontroller.SetBool("OpenAction", false);
            lidstatus = "Closed";
            pipetteguidebutton.SetActive(false);
        }

        else if (lidstatus == "Closed")
        {
            lidcontroller.SetBool("OpenAction", true);
            lidcontroller.SetBool("CloseAction", false);
            lidstatus = "AlreadyOpen";
            pipetteguidebutton.SetActive(true);
        }

        else if (lidstatus == "Open" && tipintube == false)
        {
            lidcontroller.SetBool("InitialClose", true);
            lidstatus = "Closed";
            pipetteguidebutton.SetActive(false);
        }
    }


}
