using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject pressSpaceTxt;
    public static GameObject _pressSpaceTxt;

    [SerializeField] int firstLevelWithTree, firstLevelWithTP, firstDarkLevel;
    public static int _firstLevelWithTree, _firstLevelWithTP, _firstDarkLevel;

    void Start()
    {
        _pressSpaceTxt = pressSpaceTxt;

        if (!GeneralManager.isInStoryMode)
        {
            DisplayPressSpace(false);
            enabled = false;
            return;
        }

        if (GameManager.i_currentLevel != 0) DisplayPressSpace(false);

        _firstLevelWithTree = firstLevelWithTree;
        _firstLevelWithTP = firstLevelWithTP;
        _firstDarkLevel = firstDarkLevel;
    }

    public static void DisplayTutorial()
    {
        int currentLevel = GameManager.i_currentLevel;

        if (currentLevel == _firstLevelWithTree) DisplayTreeTuto();
        else if (currentLevel == _firstLevelWithTP) DisplayTPTuto();
        else if (currentLevel == _firstDarkLevel) DisplayDarkTuto();
    }

    public static void DisplayTreeTuto() { Debug.Log("tree tuto"); }
    public static void DisplayTPTuto() { Debug.Log("tp tuto"); }
    public static void DisplayDarkTuto() { Debug.Log("dark tuto"); }

    public static void DisplayPressSpace(bool display)
    {
        _pressSpaceTxt.SetActive(display);
    }
}
