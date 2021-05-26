using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject pressSpaceTxt;
    public static GameObject _pressSpaceTxt;

    void Start()
    {
        _pressSpaceTxt = pressSpaceTxt;

        if (!GeneralManager.isInStoryMode)
        {
            DisplayPressSpace(false);
            enabled = false;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DisplayPressSpace(bool display)
    {
        _pressSpaceTxt.SetActive(display);
    }
}
