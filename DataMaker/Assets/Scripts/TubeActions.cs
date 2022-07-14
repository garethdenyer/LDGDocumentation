using UnityEngine;
using TMPro;

public class TubeActions : MonoBehaviour
{
    // Attached to Tube

    Animator lidcontroller;
    public string lidstatus;

    public TMP_Text tubelabel;

    private void Start()
    {
        lidstatus = "Open";
        lidcontroller = GetComponent<Animator>();
        LidActions();
    }

    public void LidActions()
    {
        bool tipintube = gameObject.GetComponentInChildren<PipetteGuide>().tipengaged;

        if (lidstatus == "AlreadyOpen" && tipintube==false)
        {
            lidcontroller.SetBool("CloseAction", true);
            lidcontroller.SetBool("OpenAction", false);
            lidstatus = "Closed";
        }

        else if (lidstatus == "Closed")
        {
            lidcontroller.SetBool("OpenAction", true);
            lidcontroller.SetBool("CloseAction", false);
            lidstatus = "AlreadyOpen";
        }

        else if (lidstatus == "Open" && tipintube == false)
        {
            lidcontroller.SetBool("InitialClose", true);
            lidstatus = "Closed";
        }
    }


}
