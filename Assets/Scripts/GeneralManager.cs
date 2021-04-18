using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GeneralManager : MonoBehaviour
{
    [Tooltip("Dans cette liste, référencer le nom des niveaux à charger les uns à la suite des autres.")]
    public List<string> storyLevelsName = new List<string>();
    public static string locallySavedLevelsPath, locallySavedFolder;

    [SerializeField] int fpsLimit;

    //Variables pour scène "Niveau"
    public static bool isInStoryMode;
    public static bool isInBuildMode;

    public static bool isComingFromDatabaseLevelsChoice;
    public static bool isComingFromLocalLevelsChoice;

    public static string sceneNameToLoad;

    public static Level levelToCreateChosenFromDisplayLevelsScene;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = fpsLimit;

        locallySavedFolder = "/LevelsSaved/";
        locallySavedLevelsPath = Application.persistentDataPath + locallySavedFolder;
    }

    public static void SetIsInStoryModeToTrue()
    {
        isInStoryMode = true;                       //TRUE
        isInBuildMode = false;
        isComingFromDatabaseLevelsChoice = false;
        isComingFromLocalLevelsChoice = false;
    }

    public static void SetIsInBuildModeToTrue()
    {
        isInStoryMode = false;
        isInBuildMode = true;                       //TRUE
        isComingFromDatabaseLevelsChoice = false;
        isComingFromLocalLevelsChoice = false;
    }

    public void SetIsComingFromDatabaseLevelChoiceToTrue()
    {
        isInStoryMode = false;
        isInBuildMode = false;
        isComingFromDatabaseLevelsChoice = true;    //TRUE
        isComingFromLocalLevelsChoice = false;
    }

    public void SetIsComingFromLocalLevelChoiceToTrue()
    {
        isInStoryMode = false;
        isInBuildMode = false;
        isComingFromDatabaseLevelsChoice = false;
        isComingFromLocalLevelsChoice = true;       //TRUE
    }

    public void QuitApp()
    {
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif
        }
    }
}
