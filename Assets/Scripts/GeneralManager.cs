using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GeneralManager : MonoBehaviour
{
    [Tooltip("Dans cette liste, référencer le nom des niveaux à charger.")]
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

    void Start()
    {
        if (_instance != null && _instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }

        _instance = this;

        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = fpsLimit;

        locallySavedFolder = "/LevelsSaved/";
        locallySavedLevelsPath = Application.persistentDataPath + locallySavedFolder;

        binaryDataPath = Application.persistentDataPath + "/BinaryData";

        _levelToReachToUnlockLevelCreation = levelToReachToUnlockLevelCreation;

        SetEditionsButtons();
        LoadBinaryData();

        AudioManager.Play("BackgroundMusic");
    }

    public static void SetNewGame(bool newGameValue) { newGame = newGameValue; }

    public void ResetDataSaved()
    {
        SaveSystem.SaveLevelReached(0, false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void LoadBinaryData()
    {
        GameData gameData = SaveSystem.GetBinarySavedData();
        levelReached = gameData.levelReached;

        editionUnlocked = gameData.editionUnlocked;

        if (gameData.editionUnlocked) UnlockEditionButtons();
    }

    void SetEditionsButtons()
    {
        editionsButtons.Add(GameObject.Find("Level editor").gameObject);
    }

    void UnlockEditionButtons()
    {
        foreach (GameObject go in editionsButtons)
            go.transform.GetComponent<Button>().interactable = true;
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

    public static void SetIsComingFromDatabaseLevelChoiceToTrue()
    {
        isInStoryMode = false;
        isInBuildMode = false;
        isComingFromDatabaseLevelsChoice = true;    //TRUE
        isComingFromLocalLevelsChoice = false;
    }

    public static void SetIsComingFromLocalLevelChoiceToTrue()
    {
        isInStoryMode = false;
        isInBuildMode = false;
        isComingFromDatabaseLevelsChoice = false;
        isComingFromLocalLevelsChoice = true;       //TRUE
    }

    public static void QuitApp()
    {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif
    }
}
