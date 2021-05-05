using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GeneralManager : MonoBehaviour
{
    [Tooltip("Dans cette liste, référencer le nom des niveaux à charger les uns à la suite des autres.")]
    public List<string> storyLevelsName = new List<string>();

    [SerializeField] int fpsLimit, levelToReachToUnlockLevelCreation;
    [SerializeField] List<GameObject> editionsButtons = new List<GameObject>();

    //Variables pour scène "Niveau"
    public static bool isInStoryMode, isInBuildMode, isComingFromDatabaseLevelsChoice, isComingFromLocalLevelsChoice, editionUnlocked;

    public static string locallySavedLevelsPath, locallySavedFolder, sceneNameToLoad, binaryDataPath;
    public static int levelReached, _levelToReachToUnlockLevelCreation;
    public static bool newGame;

    public static Level levelToCreateChosenFromDisplayLevelsScene;

    public static GeneralManager _instance;


    private void Start()
    {
        //if (_instance != null && _instance != this)
        //{
        //    DestroyImmediate(gameObject);
        //    Debug.Log("détruit");
        //    return;
        //}

        //_instance = this;

        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = fpsLimit;

        locallySavedFolder = "/LevelsSaved/";
        locallySavedLevelsPath = Application.persistentDataPath + locallySavedFolder;

        binaryDataPath = Application.persistentDataPath + "/BinaryData";

        _levelToReachToUnlockLevelCreation = levelToReachToUnlockLevelCreation;
        LoadBinaryData();
    }

    public void SetNewGame(bool newGameValue)
    {
        newGame = newGameValue;
    }


    void LoadBinaryData()
    {
        GameData gameData = SaveSystem.GetBinarySavedData();
        levelReached = gameData.levelReached;

        editionUnlocked = gameData.editionUnlocked;

        if (!gameData.editionUnlocked) UnlockEditionButtons();
    }

    private void UnlockEditionButtons()
    {
        foreach (GameObject go in editionsButtons)
            go.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
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
