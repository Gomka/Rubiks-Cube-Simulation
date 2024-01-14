using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChronometerController : MonoBehaviour
{
    bool isActive = false;
    float currentTime = 0f;
    public TMP_Text stopwatchText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive == true) 
        {
            currentTime += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            stopwatchText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();

        }
    }

    public void StartChrono()
    {
        if(!isActive)
        {
            currentTime = 0f;
            isActive = true;
        }
    }

    public float StopChrono()
    {
        float res = currentTime;
        isActive = false;
        currentTime = 0f;
        stopwatchText.text = "0:0:000";
        return res;
    }
}
