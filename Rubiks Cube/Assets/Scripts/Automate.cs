using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automate : MonoBehaviour
{
    public static List<string> moveList = new List<string>(){};
    private readonly List<string> allMoves = new List<string>()
    {
        "U", "U'", "L", "L'", "F", "F'", 
        "R", "R'", "B", "B'", "D", "D'"
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
            move = Random.Range(0, allMoves.Count);
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
            default: break;
        }
    }

    void RotateSide(List<GameObject> side, float angle)
    {
        PivotRotation pr = side[4].transform.parent.GetComponent<PivotRotation>();
        pr.StartAutoRotate(side, angle);
    }
}
