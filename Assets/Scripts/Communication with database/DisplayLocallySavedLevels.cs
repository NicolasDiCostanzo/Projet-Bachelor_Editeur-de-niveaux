using System.IO;
using UnityEngine;

public class DisplayLocallySavedLevels : MonoBehaviour
{
    void OnEnable()
    {
        if (!GeneralManager.isComingFromLocalLevelsChoice) GeneralManager.isComingFromLocalLevelsChoice = true;

        string directory = SaveLoadLevelData.directoryDownloadedLevels;
        string path = Application.persistentDataPath + directory;

        Debug.Log(path);

        if (Directory.Exists(path))
        {
            foreach (string file in Directory.GetFiles(path))
            {
                Level _level = new Level();

                string levelJson = File.ReadAllText(file);
                _level = JsonUtility.FromJson<Level>(levelJson);

                DisplayLevelManager.levelsToDisplay.Add(_level);
            }

            GetComponent<DisplayLevelManager>().f_DisplayLevels();
        }
    }
}
