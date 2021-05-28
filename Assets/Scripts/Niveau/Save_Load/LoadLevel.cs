using UnityEngine;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public void f_LoadLevel()
    {
        Debug.Log("loadLevel");

        string levelName = GameObject.Find("LevelName_InputField").GetComponent<InputField>().text;

        GameObject.Find("Game Manager").GetComponent<GameManager>().EraseThisLevel();
        SaveLoadLevelData.LoadFromSavedLevelsDirectory(levelName);
    }
}
