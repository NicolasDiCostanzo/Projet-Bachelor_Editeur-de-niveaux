using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveTextToLevelStructure : MonoBehaviour
{
    public void SaveDescription() { SaveLoadLevelData.levelToSave.description = GetComponent<Text>().text; }

    public void SaveClue() { SaveLoadLevelData.levelToSave.clue = GetComponent<Text>().text; }
}
