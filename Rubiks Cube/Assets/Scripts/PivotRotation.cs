using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour
{
    private List<GameObject> activeSide;
    private Vector3 localForward, mouseRef, rotation;
    private bool dragging = false, autoRotating = false;
    private float sensitivity = 0.2f, speed = 500f;

    private Quaternion targetQuaternion;
    private ReadCube readCube;
    private CubeState  cubeState;
    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    // Late Update is called once per frame at the end of Update
    void LateUpdate()
    {
        if(dragging && !autoRotating)
        {
            SpinSide(activeSide);
            if(Input.GetMouseButtonUp(0))
            {
                dragging = false;
                RotateToRightAngle();
            }
        }
        if(autoRotating)
        {
            AutoRotate();
        }
    }

    private void SpinSide (List<GameObject> side)
    {
        rotation = Vector3.zero;
        Vector3 mouseOffset = (Input.mousePosition - mouseRef);

        if(side[4].transform.name[0] == 'U')
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }
        if(side[4].transform.name[0] == 'L')
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }
        if(side[4].transform.name[0] == 'F')
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }
        if(side[4].transform.name[0] == 'R')
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }
        if(side[4].transform.name[0] == 'B')
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }
        if(side[4].transform.name[0] == 'D')
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }        

        transform.Rotate(rotation, Space.Self);
        mouseRef = Input.mousePosition;
    }

    public void Rotate(List<GameObject> side) 
    {   
        activeSide = side;
        mouseRef = Input.mousePosition;
        dragging = true;
        localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
    }

    public void StartAutoRotate(List<GameObject> side, float angle)
    {
        cubeState.PickUp(side);
        Vector3 localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
        targetQuaternion = Quaternion.AngleAxis(angle, localForward) * transform.localRotation;
        activeSide = side;
        autoRotating = true;
    } 

    public void RotateToRightAngle()
    {
        Vector3 vec = transform.localEulerAngles;
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;

        targetQuaternion.eulerAngles = vec;
        autoRotating = true;
    }

    private void AutoRotate()
    {
        dragging = false;
        var step = speed * Time.deltaTime;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);

        if(Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1)
        {
            transform.localRotation = targetQuaternion;
            cubeState.PutDown(activeSide, transform.parent);
            readCube.ReadState();
            CubeState.isTurning = false;
            autoRotating = false;
            dragging = false;
        }
    }
}
