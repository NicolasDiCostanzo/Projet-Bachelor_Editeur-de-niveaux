using UnityEngine;

public class LightManagement : MonoBehaviour
{
    public static Light lightSource;
    public static bool lightIsOn = true;
    public static GameObject squares;

    private void OnEnable() { 
        lightSource = GetComponent<Light>(); 
        squares = GameObject.Find("Squares");
    }

    public static void ToggleLight()
    {
        lightIsOn = !lightIsOn;
        if(lightSource) lightSource.enabled = lightIsOn;

        ToggleItemsRendering(lightIsOn);
    }

    public static void ToggleLight(bool lightIsSwitchedOn)
    {
        if (lightSource) lightSource.enabled = lightIsSwitchedOn;

        ToggleItemsRendering(lightIsSwitchedOn);
    }

    public static void ToggleItemsRendering(bool hasToBeRendered)
    {
        if (squares)
        {
            if (squares.transform.childCount > 0)
            {
                for (int i = 0; i < squares.transform.childCount; i++)
                {
                    GameObject square = squares.transform.GetChild(i).gameObject;

                    if (square.GetComponent<BoxDatas>().box.type == LevelBoardBoxType.Trap || square.GetComponent<BoxDatas>().box.type == LevelBoardBoxType.Tree)
                        if (square.transform.childCount > 0) square.transform.GetChild(0).GetComponent<Renderer>().enabled = hasToBeRendered;
                }
            }
        }
    }
}
