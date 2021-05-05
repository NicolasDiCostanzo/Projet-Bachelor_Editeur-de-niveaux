using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayState : MonoBehaviour
{
    [SerializeField] BuildLevelState buildLevelState_script = null;
    [SerializeField] MovementManager movementManager_script = null;
    [SerializeField] GameObject testUI = null;
    [SerializeField] Button switchStateBtn = null;
    [SerializeField] string btnText = null;

    [SerializeField] GameObject buildUI = null;
    [SerializeField] UI_Manager uiManager;

    GeneralManager generalManager_script;

    private void OnEnable()
    {
        GameManager.state = State.Play;

        if(GameObject.Find("General Manager")) generalManager_script = GameObject.Find("General Manager").GetComponent<GeneralManager>();

        if (GeneralManager.isInBuildMode) GameObject.Find("Save Manager").GetComponent<SaveLoadLevelData>().RecordLevelData();

        buildUI.SetActive(false);

        buildLevelState_script.enabled = false;
        testUI.SetActive(true);

        switchStateBtn.GetComponentInChildren<Text>().text = btnText;
        movementManager_script.enabled = true;

        StartToPlay();
    }

    private void OnDisable()
    {
        bool editionUnlocked = GeneralManager.editionUnlocked;

        if (GameManager.i_currentLevel >= GeneralManager._levelToReachToUnlockLevelCreation) editionUnlocked = true;

        SaveSystem.SaveLevelReached(GameManager.i_currentLevel, editionUnlocked);

        Debug.Log(GameManager.i_currentLevel);

        buildLevelState_script.enabled = true;

        if (testUI) testUI.SetActive(false);

        GameManager.currentTurn = 0;

        if (generalManager_script && GameManager.i_currentLevel < generalManager_script.storyLevelsName.Count)
        {
            if (!GameManager.level.isInDarkMode) LightManagement.ToggleLight(true);

            //Entre dans le 'if' ci-dessous si on vient de terminer le dernier niveau du mode histoire
            if (GameManager.i_currentLevel == generalManager_script.storyLevelsName.Count - 1 && GeneralManager.isInBuildMode == false) return;

            if (GameManager.i_currentLevel < generalManager_script.storyLevelsName.Count && GeneralManager.isInBuildMode == false)
                RestartLevel(false);
            else
                RestartLevel(true);
        }
        else //Si on rentre dans ce 'else', c'est que tous les niveaux sont terminés
        {
            GameManager.i_currentLevel = 0;
        }
    }

    List<GameObject> objectBlinkig_list = new List<GameObject>();
    List<GameObject> customizedLife_list = new List<GameObject>();

    public void StartToPlay()
    {
        if (objectBlinkig_list.Count > 0) objectBlinkig_list.Clear();
        if (customizedLife_list.Count > 0) customizedLife_list.Clear();

        GameManager.currentTurn = 0;

        if (!GeneralManager.isInBuildMode) uiManager.DeactiveNotAuthorizedFunctionalities();

        int n = GameManager.level.h * GameManager.level.w;

        GameObject boxesParent = GameObject.Find("Squares");

        for (int i = 0; i < n; i++)
        {
            if (GameManager.level.boxes[i].type == LevelBoardBoxType.Trap || GameManager.level.boxes[i].type == LevelBoardBoxType.Tree)
            {
                GameObject go = boxesParent.transform.GetChild(i).GetChild(0).gameObject;

                if (go.GetComponent<ObjectBlinking>() && go.GetComponent<ObjectBlinking>().isActiveAndEnabled)
                    objectBlinkig_list.Add(go);


                if (go.GetComponent<CustomizeObjectLife>() && go.GetComponent<CustomizeObjectLife>().isActiveAndEnabled)
                    customizedLife_list.Add(go);
            }
        }

        if (generalManager_script && GameManager.i_currentLevel < generalManager_script.storyLevelsName.Count) RestartLevel(false);
    }

    public void RestartLevel(bool goToBuildMode)
    {
        GameManager.currentTurn = 0;
        movementManager_script.ResetNbOfMovesDisplayed();

        GameObject[] redSquares = GameObject.FindGameObjectsWithTag("Red square");

        for (int i = 0; i < redSquares.Length; i++)
        {
            Destroy(redSquares[i]);
        }


        if (!goToBuildMode)
        {
            ReinitializeDynamicsObjects();

            if (GameManager.level.isInDarkMode || (GameManager.level.isInDarkMode && !LightManagement.lightIsOn)) LightManagement.ToggleLight(false);

            if (GameManager.levelCompleted) GameManager.levelCompleted = false;
            else                            GameManager.RepositionBothCharacters();
        }
        else
        {
            GameManager.RepositionBothCharacters();
            DisplayDynamicsObjects();
        }
    }

    void DisplayDynamicsObjects()
    {
        if(objectBlinkig_list.Count > 0)
        {
            foreach (GameObject go in objectBlinkig_list)
                if(go) go.GetComponent<ObjectBlinking>().ActiveObject(false);
        }

        if (customizedLife_list.Count > 0)
        {
            foreach (GameObject go in customizedLife_list)
                if (go) go.GetComponent<CustomizeObjectLife>().ActiveObject();
        }

    }

    void ReinitializeDynamicsObjects()
    {
            foreach (GameObject go in objectBlinkig_list)
                if(go) go.GetComponent<ObjectBlinking>().DetermineState();

            foreach (GameObject go in customizedLife_list)
                if (go) go.GetComponent<CustomizeObjectLife>().ObjectLifeManagement();
    }
}