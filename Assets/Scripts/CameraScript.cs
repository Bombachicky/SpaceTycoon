using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Range(0,0.06f)][SerializeField] float speed; // how fast camera moves back and forth
    [Range(0, 10.0f)][SerializeField] float zoomSpeed; // how fast scroll up and down
    [SerializeField] float rotateSpeed; // how fast camera spins around on axes

    [Range(0, 40.0f)][SerializeField] float maxHeight;
    [Range(0, 4.0f)][SerializeField] float minHeight;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = .06f;
            zoomSpeed = 20.0f;
        }else
        {
            speed = 0.035f;
            zoomSpeed = 10.0f;
        }

        // getaxis checks wasd and arrow keys
        float hsp = transform.position.y * speed * Input.GetAxis("Horizontal"); //horizontal movement variable
        float vsp = transform.position.y * speed * Input.GetAxis("Vertical"); // vertical movement
        float scrollSp = Mathf.Log(transform.position.y) * -zoomSpeed * Input.GetAxis("Mouse ScrollWheel");


        if ((transform.position.y >= maxHeight) && (scrollSp > 0))
        {
            scrollSp = transform.position.y - maxHeight;
        }
        else if ((transform.position.y <= minHeight) && (scrollSp < 0))
        {
            scrollSp = 0;
        }

        if ((transform.position.y + scrollSp) > maxHeight)
        {
            scrollSp = maxHeight - transform.position.y;
        }
        else if ((transform.position.y + scrollSp) < minHeight)
        {
            scrollSp = minHeight - transform.position.y;
        }

        Vector3 verticalMove = new Vector3(0, scrollSp, 0);
        Vector3 horizontalMove = hsp * transform.right;
        Vector3 forwardMove = transform.forward;

        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= vsp;

        Vector3 move = verticalMove + horizontalMove + forwardMove;

        transform.position += move;

    }
}
