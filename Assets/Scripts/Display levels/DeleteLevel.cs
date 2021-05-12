using System.IO;
using UnityEngine;

public class DeleteLevel : MonoBehaviour
{
    string locallySavedLevelsPath;
    private void Start()
    {
        if (GeneralManager.isComingFromLocalLevelsChoice) locallySavedLevelsPath = GeneralManager.locallySavedLevelsPath;
    }

    public void EraseLocallySavedLevel()
    {
        string levelName = GetComponentInParent<WindowConstructor>().levelName;

        if (!GeneralManager.isComingFromLocalLevelsChoice)
            StartCoroutine(GameObject.Find("Game Manager").GetComponent<DataBaseRequest>().DeleteLevel(levelName));
        else
        {
            File.Delete(locallySavedLevelsPath + levelName + ".txt");
            Debug.Log(locallySavedLevelsPath + levelName + ".txt");
            Debug.Log("delete file");
        }

        Destroy(transform.parent.gameObject);
    }
}
