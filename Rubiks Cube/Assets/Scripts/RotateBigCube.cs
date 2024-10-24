using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBigCube : MonoBehaviour
{
    private Vector2 firstPressPos, secondPressPos, currentSwipe;
    [SerializeField] GameObject target;
    [SerializeField] float rotationSpeed = 0.1f;
    [SerializeField] Camera cam;
    ReadCube readCube;

    float speed = 800f;
    

    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        Drag();
    }

    void Drag()
    {
        if (CubeState.isTurning) return;

        if (Input.GetMouseButton(1)) 
        {   
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed;
            Vector3 right = Vector3.Cross(cam.transform.up, transform.position - cam.transform.position);
            Vector3 up = Vector3.Cross(transform.position - cam.transform.position, right);
            transform.rotation = Quaternion.AngleAxis(-rotX, up) * transform.rotation;
            transform.rotation = Quaternion.AngleAxis(rotY, right) * transform.rotation;
        }
        else 
        {
            if (transform.rotation != target.transform.rotation)
            {
                var step = speed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards (transform.rotation, target.transform.rotation, step);
                readCube.ReadState();
            }
        }
    }

    void Swipe() 
    {
        if (CubeState.isTurning) return;

        if (Input.GetMouseButtonDown(1))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if(Input.GetMouseButtonUp(1))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 difference = firstPressPos.normalized - secondPressPos.normalized;

            if(difference.magnitude < 0.03f) return;
            
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            currentSwipe.Normalize();
            if(LeftSwipe(currentSwipe))
            {
                target.transform.Rotate(0, 90, 0, Space.World);
            }
            else if(RightSwipe(currentSwipe))
            {
                target.transform.Rotate(0, -90, 0, Space.World);
            }
            else if(UpLeftSwipe(currentSwipe))
            {
                target.transform.Rotate(-90, 0, 0, Space.World);
            }
            else if(UpRightSwipe(currentSwipe))
            {
                target.transform.Rotate(0, 0, 90, Space.World);
            }
            else if(DownLeftSwipe(currentSwipe))
            {
                target.transform.Rotate(0, 0, -90, Space.World);
            }
            else if(DownRightSwipe(currentSwipe))
            {
                target.transform.Rotate(90, 0, 0, Space.World);
            }

        }
        if(Input.GetKeyDown("left")){
            target.transform.Rotate(0, 90, 0, Space.World);
        }
        if(Input.GetKeyDown("right")){
            target.transform.Rotate(0, -90, 0, Space.World);
        }
        if(Input.GetKeyDown("up")){
            target.transform.Rotate(0, 0, 90, Space.World);
        }
        if(Input.GetKeyDown("down")){
            target.transform.Rotate(0, 0, -90, Space.World);
        }
    }

    bool LeftSwipe (Vector2 swipe)
    {
        return currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }

    bool RightSwipe (Vector2 swipe)
    {
        return currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }
    bool UpLeftSwipe (Vector2 swipe) 
    {
        return currentSwipe.y > 0 && currentSwipe.x < 0f;
    }
    bool UpRightSwipe (Vector2 swipe) 
    {
        return currentSwipe.y > 0 && currentSwipe.x > 0f;
    }
    bool DownLeftSwipe (Vector2 swipe) 
    {
        return currentSwipe.y < 0 && currentSwipe.x < 0f;
    }
    bool DownRightSwipe (Vector2 swipe) 
    {
        return currentSwipe.y < 0 && currentSwipe.x > 0f;
    }
}
