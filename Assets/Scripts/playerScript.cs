using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;


        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(Input.mousePosition, Input.mousePosition, out hit))
            {
                if (hit.collider.GetComponent<GameObject>() != null)
                {

                }
            }
        }
    }
}
