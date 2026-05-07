using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFace : MonoBehaviour
{
    CubeState cubeState;
    ReadCube readCube;
    int layerMask = 1 << 6;
    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)&& !CubeState.isTurning)
        {
            readCube.ReadState();

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 100.0f, layerMask) && !CubeState.isTurning)
            {
                GameObject face = hit.collider.gameObject;

                List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.up,
                    cubeState.left,
                    cubeState.front,
                    cubeState.right,
                    cubeState.back,
                    cubeState.down
                };

                foreach (List<GameObject> cubeSide in cubeSides)
                {
                    if(cubeSide.Contains(face)) 
                    {
                        cubeState.PickUp(cubeSide);
                        // Start the side rotation logic
                        // Debug.Log(cubeSide[4].transform.name);
                        cubeSide[4].transform.parent.GetComponent<PivotRotation>().Rotate(cubeSide);
                    }
                }
            }
        }
    }
}
