using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private Camera cam;
    private float hue = 0;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
    }

    // Update is called once per frame
    void Update()
    {
        hue = (hue + Time.deltaTime*0.1f) % 1; 
        cam.backgroundColor = Color.HSVToRGB(hue, 0.5f, 0.5f);
    }
}
