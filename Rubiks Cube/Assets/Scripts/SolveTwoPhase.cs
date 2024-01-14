using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;

public class SolveTwoPhase : MonoBehaviour
{
    private ReadCube readCube;
    private CubeState cubeState;
    private bool firstTime = true;

    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Solver()
    {
        readCube.ReadState();

        // Solve the cube using Kociemba's 2 phase algorythm
        // First we get the cubeState, we apply the algorythm and we convert the solved moves to a list of moves

        string moveString = cubeState.GetStateString();
        Debug.Log(moveString);

        string info = "";
        string solution = "";
        
        if(firstTime) 
        {
            solution = SearchRunTime.solution(moveString, out info, buildTables: true);
            firstTime = false;
        } else 
        {
            solution = Search.solution(moveString, out info);
        }

        List<string> solutionList = StringToList(solution);
        Debug.Log("Solution: " + solution);
        Automate.moveList = solutionList;
        Debug.Log(info);
    }

    List<string> StringToList(string solution)
    {
        return new List<string>(solution.Split(new string[]{" "}, System.StringSplitOptions.RemoveEmptyEntries));
    }
}
