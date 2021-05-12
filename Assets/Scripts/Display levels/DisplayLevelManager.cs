using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class DisplayLevelManager : MonoBehaviour
{
    DataBaseRequest getDataFromDB_script;
    DisplayLocallySavedLevels getDataFromLocal_script;

    public static List<Level> levelsToDisplay = new List<Level>();
    public static List<string> jsons = new List<string>();

    [SerializeField] GameObject window;
    [SerializeField] GameObject windowsParent;

    public static TextMeshProUGUI descriptionTMP;

    List<GameObject> windows = new List<GameObject>();

    private void Awake()
    {
        descriptionTMP = GameObject.Find("Description TMP").GetComponent<TextMeshProUGUI>();

        getDataFromDB_script = GetComponent<DataBaseRequest>();
        getDataFromLocal_script = GetComponent<DisplayLocallySavedLevels>();

        if (GeneralManager.isComingFromDatabaseLevelsChoice) getDataFromDB_script.enabled = true;
        else if(GeneralManager.isComingFromLocalLevelsChoice) getDataFromLocal_script.enabled = true;
    }

    public void f_DisplayLevels()
    {
        Debug.Log(levelsToDisplay.Count);

        for (int i = 0; i < levelsToDisplay.Count; i++)
        {
            Level newLevel = levelsToDisplay[i];
            GameObject windowInstance = Instantiate(window, windowsParent.transform);
            windows.Add(windowInstance);
            windowInstance.name = "Level data " + (i + 1);

            windowInstance.GetComponent<WindowConstructor>().CreateWindow(newLevel);
        }

        ReinitData();
    }

    public void EraseAllLevelsDisplayed() { 
        for (int i = 0; i < windows.Count; i++) 
            Destroy(windows[i]);
    }

    void ReinitData()
    {
        levelsToDisplay.Clear();
        jsons.Clear();

        getDataFromDB_script.enabled = false;
        getDataFromLocal_script.enabled = false;
    }

    void OnDisable() { ReinitData(); }
}
