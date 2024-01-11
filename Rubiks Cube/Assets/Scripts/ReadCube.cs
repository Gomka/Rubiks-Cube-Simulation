using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCube : MonoBehaviour
{
    [SerializeField] public Transform tUp, tLeft, tFront, tRight, tBack, tDown;

    private int layerMask = 1 << 6; // layer of the cube faces
    CubeState cubeState;
    CubeMap cubeMap;
    public GameObject emptyGO;

    private List<GameObject> upRays = new List<GameObject>();
    private List<GameObject> leftRays = new List<GameObject>();
    private List<GameObject> frontRays = new List<GameObject>();
    private List<GameObject> rightRays = new List<GameObject>();
    private List<GameObject> backRays = new List<GameObject>();
    private List<GameObject> downRays = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        SetRayTransforms();
        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadState();
    }

    public void ReadState() 
    {
        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();

        cubeState.up = ReadFace(upRays, tUp);
        cubeState.left = ReadFace(leftRays, tLeft);
        cubeState.front = ReadFace(frontRays, tFront);
        cubeState.right = ReadFace(rightRays, tRight);
        cubeState.back = ReadFace(backRays, tBack);
        cubeState.down = ReadFace(downRays, tDown);

        cubeMap.Set();
    }

    void SetRayTransforms() 
    {
        upRays = BuildRays(tUp, new Vector3(90, 90, 0));
        leftRays = BuildRays(tLeft, new Vector3(0, 0, 0));
        frontRays = BuildRays(tFront, new Vector3(180, 90, 0));
        rightRays = BuildRays(tRight, new Vector3(0, 180, 0));
        backRays = BuildRays(tBack, new Vector3(0, 90, 0));
        downRays = BuildRays(tDown, new Vector3(270, 90, 0));
    }

    List<GameObject> BuildRays(Transform rayTrasnform, Vector3 direction) 
    {
        int rayCount = 0;
        List<GameObject> rays = new List<GameObject>();

        // 9 rays each facing one side of the cube in the following order:
        // |0|1|2|
        // |3|4|5|
        // |6|7|8|

        for(int y = 1; y > -2; y--) 
        {
            for (int x = -1; x < 2; x++)
            {
                Vector3 startPos = new Vector3(rayTrasnform.position.x + x, rayTrasnform.position.y + y, rayTrasnform.position.z);
                GameObject rayStart = Instantiate(emptyGO, startPos, Quaternion.identity, rayTrasnform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }

        rayTrasnform.localRotation = Quaternion.Euler(direction);
        return rays;
    }

    public List<GameObject> ReadFace(List<GameObject> rayStarts, Transform rayTrasnform)
    {
        List<GameObject> facesHit = new List<GameObject>();

        foreach(GameObject rayStart in rayStarts)
        {
            Vector3 ray = tFront.transform.position;
            RaycastHit hit;

            if(Physics.Raycast(ray, rayTrasnform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(ray, rayTrasnform.forward * hit.distance, Color.yellow);
                facesHit.Add(hit.collider.gameObject);
                Debug.Log("Hit!" + ray);
            }
            else 
            {
                Debug.DrawRay(ray, rayTrasnform.forward * 1000, Color.green);
            }
        }

        return facesHit;
    }
}
