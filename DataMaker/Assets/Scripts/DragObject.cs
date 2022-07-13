using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    GameObject theTube;

    void OnMouseDown()
    {
        //get the z coordinate of the mouse
        theTube = transform.parent.gameObject;
        mZCoord = Camera.main.WorldToScreenPoint(theTube.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = theTube.transform.position - GetMouseAsWorldPoint();
    }


    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }


    void OnMouseDrag()
    {
        if (!theTube.GetComponent<PipetteGuide>().tipengaged)
        {
            theTube.transform.position = 
                new Vector3(GetMouseAsWorldPoint().x + mOffset.x, theTube.transform.position.y, GetMouseAsWorldPoint().z + mOffset.z);
        }
    }
}
