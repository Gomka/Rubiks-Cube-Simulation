using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeState : MonoBehaviour
{
    public List<GameObject> up = new List<GameObject>();
    public List<GameObject> left = new List<GameObject>();
    public List<GameObject> front = new List<GameObject>();
    public List<GameObject> right = new List<GameObject>();
    public List<GameObject> back = new List<GameObject>();
    public List<GameObject> down = new List<GameObject>();

    public static bool isTurning = false, started = false, scrambled = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp(List<GameObject> cubeSide)
    {
        if(isTurning && !started) return;
        isTurning = true;
        foreach(GameObject face in cubeSide)
        {
            // Attach the cubie of each face
            // to the parent of the 4th index (the center cubie)
            // unless it's already the 4th index

            if(face != cubeSide[4])
            {
                face.transform.parent.transform.parent = cubeSide[4].transform.parent;
            }
        }
    }

    public void PutDown(List<GameObject> cubies, Transform pivot)
    {
        //if(isTurning) return;
        foreach(GameObject cubie in cubies)
        {
            if(cubie != cubies[4])
            {
                cubie.transform.parent.transform.parent = pivot;
            }
        }
    }

    string GetSideString(List<GameObject> side)
    {   
        string sideString = "";
        foreach (GameObject face in side)
        {
            sideString += face.name[0].ToString();
        }
        return sideString;
    }

    public string GetStateString()
    {
        string stateString = "";
        char ch_up, ch_right, ch_front, ch_down, ch_left, ch_back;

        stateString += GetSideString(up);
        stateString += GetSideString(right);
        stateString += GetSideString(front);
        stateString += GetSideString(down);
        stateString += GetSideString(left);
        stateString += GetSideString(back);

        ch_up = up[4].name[0];
        ch_right = right[4].name[0];
        ch_front = front[4].name[0];
        ch_down = down[4].name[0];
        ch_left = left[4].name[0];
        ch_back = back[4].name[0];

        stateString = stateString.Replace(ch_up, '1');
        stateString = stateString.Replace(ch_left, '2');
        stateString = stateString.Replace(ch_front, '3');
        stateString = stateString.Replace(ch_right, '4');
        stateString = stateString.Replace(ch_back, '5');
        stateString = stateString.Replace(ch_down, '6');

        stateString = stateString.Replace('1', 'U');
        stateString = stateString.Replace('2', 'L');
        stateString = stateString.Replace('3', 'F');
        stateString = stateString.Replace('4', 'R');
        stateString = stateString.Replace('5', 'B');
        stateString = stateString.Replace('6', 'D');

        return stateString;
    }
}
