using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap : MonoBehaviour
{

    CubeState cubeState;

    public Transform up, left, front, right, back, down;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set()
    {
        cubeState = FindObjectOfType<CubeState>();

        UpdateMap(cubeState.up, up);
        UpdateMap(cubeState.left, left);
        UpdateMap(cubeState.front, front);
        UpdateMap(cubeState.right, right);
        UpdateMap(cubeState.back, back);
        UpdateMap(cubeState.down, down);
    }

    void UpdateMap(List<GameObject> face, Transform side) 
    {   
        int i = 0;
        foreach (Transform map in side) 
        {

            if(map.name[0] == 'T' || map.name[0] == 'B' || face.Count < 9) break;

            switch (face[i].name[0])
            {
                case 'U': map.GetComponent<Image>().color = Color.white; break;
                case 'L': map.GetComponent<Image>().color = new Color(1, 0.5f, 0, 1); break;
                case 'F': map.GetComponent<Image>().color = new Color(0, 1, 0.2f, 1); break;
                case 'R': map.GetComponent<Image>().color = new Color(1, 0, 0, 1); break;
                case 'B': map.GetComponent<Image>().color = new Color(0, 0.33f, 1, 1); break;
                case 'D': map.GetComponent<Image>().color = new Color(0.95f, 1, 0, 1); break;
                default: break;
            } 

            i++;
        }
    }
}
