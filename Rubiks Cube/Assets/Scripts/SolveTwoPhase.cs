using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;

public class SolveTwoPhase : MonoBehaviour
{
    private ReadCube readCube;
    private CubeState cubeState;
    private bool doOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CubeState.started && doOnce)
        {
            doOnce = false;
            Solver();
        }
    }

    public void Solver()
    {
        readCube.ReadState();

        // Solve the cube using Kociemba's 2 phase algorythm
        // First we get the cubeState, we apply the algorythm and we convert the solved moves to a list of moves

        string moveString = cubeState.GetStateString();

        string info = "";
        string solution = "";

        //solution = SearchRunTime.solution(moveString, out info, buildTables: true);

        solution = Search.solution(moveString, out info);

        List<string> solutionList = StringToList(solution);

        if(solution.Length != 0 && solution[0]!= 'E')
        {
            Automate.moveList = solutionList;
        }        
    }

    List<string> StringToList(string solution)
    {
        return new List<string>(solution.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries));
    }
}
