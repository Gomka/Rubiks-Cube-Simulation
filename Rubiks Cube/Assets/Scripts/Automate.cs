using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automate : MonoBehaviour
{
    public static List<string> moveList = new List<string>(){};
    private readonly List<string> allMoves = new List<string>()
    {
        "U", "U'", "L", "L'", "F", "F'", 
        "R", "R'", "B", "B'", "D", "D'",
        "U2", "L2", "F2", "R2", "B2", "D2"
    };
    private CubeState cubeState;
    private ReadCube readCube;
    // Start is called before the first frame update
    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        readCube = FindObjectOfType<ReadCube>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveList.Count > 0 && !CubeState.isTurning && CubeState.started)
        {
            DoMove(moveList[0]);
            moveList.Remove(moveList[0]);
        }
    }

    public void Shuffle()
    {   
        int randomMove = -1, move;
        List<string> moves = new List<string>();
        int shuffleLength = Random.Range(20, 30);
        for (int i = 0; i < shuffleLength;)
        {
            move = Random.Range(0, allMoves.Count - 6);
            if(Mathf.Abs(randomMove - move) != 1)
            {
                randomMove = move;
                moves.Add(allMoves[randomMove]);
                i++;
            }
        }
        moveList = moves;
    }

    public void DoMove(string move)
    {
        if(CubeState.isTurning) return;
        readCube.ReadState();
        CubeState.isTurning = true;
        switch (move)
        {
            case "U": RotateSide(cubeState.up, -90); break;
            case "U'": RotateSide(cubeState.up, 90); break;
            case "L": RotateSide(cubeState.left, -90); break;
            case "L'": RotateSide(cubeState.left, 90); break;
            case "F": RotateSide(cubeState.front, -90); break;
            case "F'": RotateSide(cubeState.front, 90); break;
            case "R": RotateSide(cubeState.right, -90); break;
            case "R'": RotateSide(cubeState.right, 90); break;
            case "B": RotateSide(cubeState.back, -90); break;
            case "B'": RotateSide(cubeState.back, 90); break;
            case "D": RotateSide(cubeState.down, -90); break;
            case "D'": RotateSide(cubeState.down, 90); break;
            case "U2": RotateSide(cubeState.up, -180); break;
            case "L2": RotateSide(cubeState.left, -180); break;
            case "F2": RotateSide(cubeState.front, -180); break;
            case "R2": RotateSide(cubeState.right, -180); break;
            case "B2": RotateSide(cubeState.back, -180); break;
            case "D2": RotateSide(cubeState.down, -90); break;
            default: break;
        }
    }

    void RotateSide(List<GameObject> side, float angle)
    {
        //Debug.Log(side.Count);
        if(side == null) return;

        PivotRotation pr = side[4].transform.parent.GetComponent<PivotRotation>();
        pr.StartAutoRotate(side, angle);
    }
}
