using UnityEngine;
using System.Collections;

public class DragingScript : MonoBehaviour
{

    // http://k2nicestudio.com/tutorial/2016/1/31/tutorial-1-click-or-touch-to-drag-an-object
    // Siglhy modified to use rigidbody2d...
    //This code is for 2D click/drag gameobject
    //Please make sure to change Camera Projection to Orthographic
    //Add Collider (not 2DCollider) to gameObject  

    public GameObject gameObjectTodrag; //refer to GO that being dragged

    public Vector3 GOcenter; //gameobjectcenter
    public Vector3 touchPosition; //touch or click position
    public Vector3 offset;//vector between touchpoint/mouseclick to object center
    public Vector3 newGOCenter; //new center of gameObject

    RaycastHit hit; //store hit object information

    public bool draggingMode = false;
    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //***********************
        // *** CLICK TO DRAG ****
        //***********************

#if UNITY_EDITOR
        //first frame when user click left mouse
        if (Input.GetMouseButtonDown(0))
        {
            //convert mouse click position to a ray
           // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //if ray hit a Collider ( not 2DCollider)
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);// Changed to conserv 2d stuff related like rigidbody2d

            //if ray hit a Collider ( not 2DCollider)
            // if (Physics.Raycast(ray, out hit))
            if (hit.collider != null)
            {
                gameObjectTodrag = hit.collider.gameObject;
                GOcenter = gameObjectTodrag.transform.position;
                touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                offset = touchPosition - GOcenter;
                draggingMode = true;
            }
        }

        //every frame when user hold on left mouse
        if (Input.GetMouseButton(0))
        {
            if (draggingMode)
            {
                touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newGOCenter = touchPosition - offset;
                gameObjectTodrag.transform.position = new Vector3(newGOCenter.x, newGOCenter.y, GOcenter.z);
            }
        }

        //when mouse is released
        if (Input.GetMouseButtonUp(0))
        {
            draggingMode = false;
        }
#endif

        //***********************
        // *** TOUCH TO DRAG ****
        //***********************
        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                //When just touch
                case TouchPhase.Began:
                    //convert mouse click position to a ray
                    //Ray ray = Camera.main.ScreenPointToRay(touch.position);

                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);// Changed to conserv 2d stuff related like rigidbody2d

                    //if ray hit a Collider ( not 2DCollider)
                    // if (Physics.Raycast(ray, out hit))
                    if (hit.collider != null)
                    {
                        gameObjectTodrag = hit.collider.gameObject;
                        if (gameObjectTodrag.tag != "Player Boundary") { 
                        GOcenter = gameObjectTodrag.transform.position;
                        touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        offset = touchPosition - GOcenter;
                        draggingMode = true;
                        }
                    }
                    break;

                case TouchPhase.Moved:
                    if (draggingMode)
                    {
                        touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        newGOCenter = touchPosition - offset;
                        gameObjectTodrag.transform.position = new Vector3(newGOCenter.x, newGOCenter.y, GOcenter.z);
                    }
                    break;

                case TouchPhase.Ended:
                    draggingMode = false;
                    break;
            }
        }
    }
}