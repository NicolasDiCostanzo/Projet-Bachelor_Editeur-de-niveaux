using UnityEngine;

public class LightManagement : MonoBehaviour
{
    public static Light lightSource;
    public static bool lightIsOn = true;

    private void OnEnable() { lightSource = GetComponent<Light>(); }

    public static void ToggleLight()
    {
        lightIsOn = !lightIsOn;
        if(lightSource) lightSource.enabled = lightIsOn;
    }

    public static void ToggleLight(bool isOn)
    {
        if (lightSource) lightSource.enabled = isOn;
    }
}
