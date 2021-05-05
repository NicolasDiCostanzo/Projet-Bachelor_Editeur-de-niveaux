//#define TEST

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static State state;
    GeneralManager generalManager_script;
    public static int i_currentLevel;

    public GameObject Plateau;
    [SerializeField] GameObject switchState_btn;
    [SerializeField] GameObject save_btn;
    [SerializeField] GameObject pausePanel;

    [SerializeField] GameObject descriptionGO;
    [SerializeField] GameObject clueGO;

    public List<Level> levels;

    public int w;
    public int h;
    public int BOX_SIZE = 1; //Pas réussi à récupérer quand constante

    public static Level level = new Level();

    MovementManager _MM;
    BuildManager _BM;
    PlayState playState_script;
    GameObject statesManagers_go;
    SwitchState switchState_script;

    public static int currentTurn = 0;      //nombre de coups déjà faits
    public static int nbOfMovesLimit = 0;   //nombre de coups max autorisés
    public static GameObject boxesParent;
    public static GameObject player;
    public static GameObject witch;
    public static bool levelCompleted = false;

    [SerializeField] GameObject maxMoves_go;

    bool isInBuildMode;
    bool isInStoryMode;
    bool isComingFromLocalLevelChoice;

    string levelNameToStartWith;

    void Awake()
    {
        _MM = GetComponent<MovementManager>();
        _BM = GetComponent<BuildManager>();
        statesManagers_go = GameObject.Find("States Managers");


        isInBuildMode = GeneralManager.isInBuildMode;
        isInStoryMode = GeneralManager.isInStoryMode;
        isComingFromLocalLevelChoice = GeneralManager.isComingFromLocalLevelsChoice;

#if TEST
isInBuildMode = true;
#else
        generalManager_script = GameObject.Find("General Manager").GetComponent<GeneralManager>();
#endif
        playState_script = statesManagers_go.GetComponent<PlayState>();

        boxesParent = GameObject.Find("Squares");

        switchState_script = statesManagers_go.GetComponent<SwitchState>();

        level.w = w;
        level.h = h;
        level.boxes = new List<LevelBoardBox>();

        int n = h * w;

        for (int i = 0; i < n; i++) level.boxes.Add(new LevelBoardBox(LevelBoardBoxType.None));
    }

    private void Start()
    {
        bool newGame = GeneralManager.newGame;

        if (!newGame)
        {
            i_currentLevel = SaveSystem.GetBinarySavedData().levelReached;

            Debug.Log(i_currentLevel);

            if (i_currentLevel <= 0) i_currentLevel = 0;

            if (i_currentLevel >= generalManager_script.storyLevelsName.Count - 1) i_currentLevel = generalManager_script.storyLevelsName.Count - 1;
        }
        else
        {
            i_currentLevel = 0;
        }

        if (isInBuildMode)
        {
            StartInBuildMode();
            return;
        }
        else
        {
            if (isInStoryMode)                     levelNameToStartWith = generalManager_script.storyLevelsName[i_currentLevel];
            else if (isComingFromLocalLevelChoice) levelNameToStartWith = GeneralManager.sceneNameToLoad;

            StartInPlayMode(levelNameToStartWith);
            
            playState_script.enabled = true;
        }
    }


    void Update() { 
        if (Input.GetKeyDown(KeyCode.Escape)) pausePanel.SetActive(!pausePanel.activeInHierarchy); 
        if (Input.GetKeyDown(KeyCode.Space) && !GeneralManager.isInBuildMode)  StartCoroutine(LevelTransition()); 
    
    }

    private void OnDisable() { 
        if(level != null) EraseLevel(level);
        GeneralManager.sceneNameToLoad = "";
    }

    public void ToggleDarkMode() { level.isInDarkMode = !level.isInDarkMode; }

    void StartInBuildMode()
    {
        state = State.Build;

        switchState_btn.SetActive(true);
        save_btn.SetActive(true);
        switchState_script.EnableBuildState();
    }

    void StartInPlayMode(string a_levelNameToStartWith)
    {
        state = State.Play;
        SaveLoadLevelData.LoadFromSavedLevelsDirectory(a_levelNameToStartWith);
    }

    public void CreateLevel(Level level)
    {
        alreadyDied = false;
        currentTurn = 0;

        GetComponent<MovementManager>().ResetNbOfMovesDisplayed();

        int n = level.w * level.h;

        BuildManager _BM = GetComponent<BuildManager>();

        nbOfMovesLimit = level.nbTurns;
        maxMoves_go.GetComponent<InputField>().text = nbOfMovesLimit.ToString();

        descriptionGO.GetComponent<TextMeshProUGUI>().text = level.description;
        clueGO.GetComponent<TextMeshProUGUI>().text = level.clue;

        if (level.isInDarkMode) {
            if (state == State.Build) GameObject.Find("DarkMode_toggle").GetComponent<Toggle>().isOn = true;
            else LightManagement.ToggleLight(false);
        }
        else
        {
            LightManagement.ToggleLight(true);
        }

        for (int i = 0; i < n; i++)
        {
            Transform currentBox = boxesParent.transform.GetChild(i);
            LevelBoardBox boxData = level.boxes[i];

            if (boxData.type != LevelBoardBoxType.None)
            {

                GameObject newObject = _BM.CreateObject(boxData.type, currentBox);

                if (newObject != null)
                {
                    LevelBoardBox newCreatedBoxDatas = newObject.transform.parent.GetComponent<BoxDatas>().box;

                    if (boxData.blinkingFrq >= 2)
                    {
                        newCreatedBoxDatas.blinkingFrq = boxData.blinkingFrq;
                        newCreatedBoxDatas.blinkingMode = boxData.blinkingMode;

                        if(newObject.GetComponent<ObjectBlinking>()) newObject.GetComponent<ObjectBlinking>().enabled = true;

                    }
                    else if (boxData.buildTurn != 0 || boxData.destroyTurn != 0)
                    {
                        newCreatedBoxDatas.buildTurn = boxData.buildTurn;
                        newCreatedBoxDatas.destroyTurn = boxData.destroyTurn;

                        if (newObject.GetComponent<CustomizeObjectLife>()) newObject.GetComponent<CustomizeObjectLife>().enabled = true;
                    }

                    if (boxData.type == LevelBoardBoxType.Player)
                    {
                        player = newObject;
                        playerIndex = boxData.index;
                    }
                    else if (boxData.type == LevelBoardBoxType.Witch)
                    {
                        witch = newObject;
                        witchIndex = boxData.index;
                    }
                }
            }
        }
    }

    public static int playerIndex;
    public static int witchIndex;

    public static void RepositionBothCharacters()
    {
        if(player) RepositionCharacter(player.transform, playerIndex);
        if(witch)  RepositionCharacter(witch.transform, witchIndex);
    }

    public static void RepositionCharacter(Transform charac_transform, int boxIndex)
    {
        GameObject originalBox = boxesParent.transform.GetChild(boxIndex).gameObject;

        if (charac_transform.parent != originalBox.transform)
        {
            charac_transform.parent = originalBox.transform;
            charac_transform.position = originalBox.transform.position + charac_transform.GetComponent<ObjectManager>().positionOffset;
        }
    }

    //Utile pour que la fonction 'EraseLevel' soit appelée par un bouton.
    public void EraseThisLevel() { EraseLevel(level); }

    void EraseLevel(Level level)
    {
        int n = level.w * level.h;

        if (boxesParent)
        {
            for (int i = 0; i < n; i++)
            {
                Transform currentBox = boxesParent.transform.GetChild(i);

                if (currentBox.childCount > 0 || currentBox.GetComponent<BoxDatas>().box.type != LevelBoardBoxType.None)
                    _BM.RemoveObjectsFromBox(currentBox);
            }
        }
    }

    public static bool alreadyDied;

    public void SetAlreadyDied(bool a_alreadyDied) { alreadyDied = a_alreadyDied; }
    public void LevelCompleted()
    {
        i_currentLevel++;

        if (isInBuildMode)
        {
            DisplayInformationMessage.Message("Level completed!");

            if ((nbOfMovesLimit == 0 || currentTurn <= nbOfMovesLimit) && !alreadyDied) GameObject.Find("Save Button").GetComponent<Button>().interactable = true;
        }
        else
        {
            levelCompleted = true;
            StartCoroutine(LevelTransition());
        }
    }

    //On ne peut pas appeler la coroutine directement depuis le script 'CharactersBehaviour' puisque ce script est détruit quand on appelle 'EraseLevel'. On passe donc par une fonction intermédiaire qui, elle, n'est jamais détruite
    public void LevelLost() { StartCoroutine(LevelTransition()); }

    void WhenCompleteLocallySavedLevel()
    {
        GameObject.Find("Win Panel").GetComponent<Animator>().enabled = true;

        string thisLevelName = levelNameToStartWith;

        string directory = SaveLoadLevelData.directoryDownloadedLevels;
        string path = Application.persistentDataPath + directory;

        if (Directory.Exists(path))
        {
            string levelPath = path + thisLevelName + ".txt";

            string levelJson = File.ReadAllText(levelPath);

            Level thisLevel = JsonUtility.FromJson<Level>(levelJson);
            thisLevel.completed = true;

            levelJson = JsonUtility.ToJson(thisLevel, true);

            File.WriteAllText(levelPath, levelJson);
        }
    }

    public IEnumerator LevelTransition()
    {
        GetComponent<MovementManager>().enabled = false;

        if (!GeneralManager.isInStoryMode && levelCompleted) WhenCompleteLocallySavedLevel();


        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        DisplayInformationMessage.HideInfoPanel();
        EraseLevel(level);

        yield return new WaitForSeconds(.5f);
        GetComponent<MovementManager>().enabled = true;

        if (isInStoryMode) StoryModeTransition();
        else
        {
            if (levelCompleted)
            {
                SceneManager.LoadScene("Menu");
            }
            else
            {
                //ManageDataFromLocalFiles.LoadFromSavedLevelsDirectory(levelNameToReload);
                //playState_script.StartToPlay();
                PlayCustomedLevelTransition();
            }
        }
    }

    void StoryModeTransition()
    {
        if (i_currentLevel < generalManager_script.storyLevelsName.Count)
        {
            string levelName = generalManager_script.storyLevelsName[i_currentLevel];
            SaveLoadLevelData.LoadFromSavedLevelsDirectory(levelName);
            GetComponent<MovementManager>().enabled = true;
            playState_script.StartToPlay();
        }
        else
        {
            SceneManager.LoadScene("End Screen");
        }
    }

    void PlayCustomedLevelTransition()
    {
        string levelName = GeneralManager.sceneNameToLoad;
        SaveLoadLevelData.LoadFromSavedLevelsDirectory(levelName);
        playState_script.StartToPlay();
    }

    public void PlayerDiesOnWitch() { DisplayInformationMessage.Message("Player dies on witch"); }

    public (int x, int y) ConvertPosToBoxes(Vector3 posToConvert)
    {
        int xBox;
        int yBox;

        Transform Board = GameObject.Find("BoardPivot(0,0)").transform;
        Vector3 boardPosition = Board.position;

        xBox = (int)(posToConvert.x - boardPosition.x);
        yBox = (int)(posToConvert.z - boardPosition.z);

        return (xBox, yBox);
    }

    public void DisplayRenderer(GameObject go) { go.GetComponent<Renderer>().enabled = true; }

    public void UndisplayRenderer(GameObject go) { go.GetComponent<Renderer>().enabled = false; }
}


public enum State
{
    Play,
    Build
}

[Serializable]
public class ResultFromDB
{
    public bool success;

    public Level[] data;
}

[Serializable]
public class Level
{
    public string creatorName;
    public string levelName;
    public string creationDate;

    public string description;
    public string clue;
    public string objectsContained;

    //Taille du plateau
    public int w;
    public int h;

    //Limite nombre de tours pour faire ce niveau
    public int nbTurns;
    public bool isInDarkMode;
    public List<LevelBoardBox> boxes = new List<LevelBoardBox>();

    public bool completed;
}

[Serializable]
public class LevelBoardBox
{
    // public LevelBoardBoxType type;
    public LevelBoardBoxType type;

    public int index;

    //Mode et fréquence d'apparition
    public int blinkingMode;
    public int blinkingFrq;

    //Tour auquel apparaître
    public int buildTurn;
    //Tour auquel disparaître
    public int destroyTurn;

    public LevelBoardBox(LevelBoardBoxType type)
    {
        this.type = type;
    }
}

[Serializable]
public enum LevelBoardBoxType
{
    None = -1,

    //Peuvent être instanciés directement via le build panel
    Player = 0,
    Witch = 1,
    Trap = 2,
    Tree = 3,
    Teleport_IN = 4,
    Fire = 5,
    Lamp = 6,

    //NE peuvent PAS être instanciés directement via le build panel
    Teleport_OUT = 7,
    DarkTrap = 8
}