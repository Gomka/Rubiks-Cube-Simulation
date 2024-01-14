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

    private ChronometerController chrono;

    private CanvasController canvasController;

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
        ReadState();
        CubeState.started = true;
        chrono = FindObjectOfType<ChronometerController>();
        canvasController = FindObjectOfType<CanvasController>();
    }

    // Update is called once per frame
    void Update()
    {
        //ReadState();
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

        if(CheckSolved())
        {
            // trigger solved state
            float time = chrono.StopChrono();
            canvasController.CubeSolved(time);
        }
    }

    void SetRayTransforms() 
    {
        upRays = BuildRays(tUp, new Vector3(90, 90, 180));
        leftRays = BuildRays(tLeft, new Vector3(0, 0, 0));
        frontRays = BuildRays(tFront, new Vector3(180, 90, 180));
        rightRays = BuildRays(tRight, new Vector3(0, 180, 0));
        backRays = BuildRays(tBack, new Vector3(0, 90, 0));
        downRays = BuildRays(tDown, new Vector3(270, 90, 180));
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
                Vector3 startPos = new Vector3(rayTrasnform.localPosition.x + x, rayTrasnform.localPosition.y + y, rayTrasnform.localPosition.z);
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
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;

            if(Physics.Raycast(ray, rayTrasnform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(ray, rayTrasnform.forward * hit.distance, Color.yellow);
                facesHit.Add(hit.collider.gameObject);
            }
            else 
            {
                Debug.DrawRay(ray, rayTrasnform.forward * 1000, Color.green);
            }
        }

        return facesHit;
    }

    public bool CheckSolved() 
    {   
        if(!CubeState.scrambled) return false;

        foreach (GameObject face in cubeState.up)
        {
            if(face.name != cubeState.up[4].name) return false;
        }
        foreach (GameObject face in cubeState.left)
        {
            if(face.name != cubeState.left[4].name) return false;
        }
        foreach (GameObject face in cubeState.front)
        {
            if(face.name != cubeState.front[4].name) return false;
        }
        foreach (GameObject face in cubeState.right)
        {
            if(face.name != cubeState.right[4].name) return false;
        }
        foreach (GameObject face in cubeState.back)
        {
            if(face.name != cubeState.back[4].name) return false;
        }
        
        CubeState.scrambled = false;
        return true;
    }
}
