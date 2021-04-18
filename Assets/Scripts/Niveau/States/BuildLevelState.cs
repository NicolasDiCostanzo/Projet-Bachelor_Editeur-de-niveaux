using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildLevelState : MonoBehaviour
{
    [SerializeField] PlayState testLevelState_script;
    [SerializeField] MovementManager movementManager_script;
    [SerializeField] Button switchStateBtn = null;
    [SerializeField] string btnText = null;

    [SerializeField] GameObject buildUI = null;

    [HideInInspector] public bool quitScene = false;

    GameManager _GM;

    private void OnEnable()
    {
        _GM = GameObject.Find("Game Manager").GetComponent<GameManager>();

        GameManager.state = State.Build;
        quitScene = false;
        testLevelState_script.enabled = false;
        buildUI.SetActive(true);
        switchStateBtn.GetComponentInChildren<Text>().text = btnText;
        movementManager_script.enabled = false;

        SaveLoadLevelData.levelToSave_json = "";
    }

    private void OnDisable()
    {
        DisplayInformationMessage.HideInfoPanel();
        testLevelState_script.enabled = true;

        //ManageDataFromLocalFiles.levelToSave_json = "";

        //if (!quitScene)
        //    if (GeneralManager.isInBuildMode) GameObject.Find("Save Manager").GetComponent<ManageDataFromLocalFiles>().RecordLevelData();
    }

    public void CheckIfLevelIsTestable()
    {
        bool levelIsTestable = true;

        if (levelIsTestable) this.enabled = false;
    }

    public void QuitSceneToTrue() { quitScene = true; }
}
