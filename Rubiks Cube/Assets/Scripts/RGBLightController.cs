using UnityEngine;

public class RGBLightController : MonoBehaviour
{
    private Light light;
    private float hue = 0;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        hue = (hue + Time.deltaTime * 0.1f) % 1;
        light.color = Color.HSVToRGB(hue, 0.4f, 1.0f);
    }
}
