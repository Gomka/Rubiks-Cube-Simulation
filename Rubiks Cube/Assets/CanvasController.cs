using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] Canvas cubeCanvas, infoCanvas, solveCanvas;
    [SerializeField] TMP_Text solveText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenInfo()
    {
        cubeCanvas.enabled = false;
        infoCanvas.enabled = true;
    }

    public void CloseInfo()
    {
        cubeCanvas.enabled = true;
        infoCanvas.enabled = false;
    }

    public void CubeSolved(float time) 
    {
        cubeCanvas.enabled = false;
        infoCanvas.enabled = false;
        solveCanvas.enabled = true;
        TimeSpan finalTime = TimeSpan.FromSeconds(time);
        solveText.text = "The cube was solved in ";
        solveText.text += finalTime.Minutes.ToString() + ":" + finalTime.Seconds.ToString() + ":" + finalTime.Milliseconds.ToString();
    }

    public void ReturnToCube()
    {
        cubeCanvas.enabled = true;
        solveCanvas.enabled = false;       
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
