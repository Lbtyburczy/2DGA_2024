using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is an example how to create a drag and drop system / mouse interaction
/// </summary>

public class DragAndDrop : MonoBehaviour
{
    // the object we want to drag around
    public GameObject selectedObject;
    // will be needed to calculate the offset from the mouse's screencoordinates to the actual world space
    Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        // first we get the mouseposition
        // the mouse position is given to us with ScreenSpace coordinates, so we have to convert them to world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) {
            // we check if there is an object with a collider at the point we press the mousebutton at
            Collider2D colliderTarget = Physics2D.OverlapPoint(mousePosition);

            if (colliderTarget)
            {
                // we bind the targeted object
                selectedObject = colliderTarget.transform.gameObject;
                // we calculate the offset so that the object we are moving stays at it's correct z coordinate
                offset = selectedObject.transform.position - mousePosition;
            }
        }

        if (selectedObject) {
            // change the position of the object we are dragging if there is one present
            selectedObject.transform.position = mousePosition + offset;
        }

        // if we currently are holding an object and let go of the mouse button we unbind the dragged object
        if (Input.GetMouseButtonUp(0) && selectedObject) {
            selectedObject = null;
        }
    }
}
