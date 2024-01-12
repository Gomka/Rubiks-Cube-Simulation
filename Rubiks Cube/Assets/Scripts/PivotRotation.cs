using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour
{
    private List<GameObject> activeSide;
    private Vector3 localForward;
    private Vector3 mouseRef;
    private bool dragging = false, isRotating = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rotate(List<GameObject> side) 
    {   
        if(isRotating) return;
        isRotating = true;

        activeSide = side;
        mouseRef = Input.mousePosition;
        dragging = true;
        localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;

        isRotating = false;
    }
}
