using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Drag : MonoBehaviour
{
    private Vector3 offset;

    private bool IsMouseDown = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(IsMouseDown)
            {
                OnMouseDrag();
            }
            else
            {
                OnMouseDown();
                IsMouseDown = true;
            }
            
        }
        else
        {
            IsMouseDown = false;
        }
    }
    private void OnMouseDown()
    {
        offset = gameObject.transform.position - Building_System.GetMouseWorldPosition();
        var x = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        Vector3 pos = Building_System.GetMouseWorldPosition() + offset;
        transform.position = Building_System.current.snap_coordinate_to_grid(pos);
    }

   
}
