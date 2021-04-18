using System.IO;
using UnityEngine;

public class DeleteLevel : MonoBehaviour
{
    string locallySavedLevelsPath;
    private void Start()
    {
        if (!GeneralManager.isComingFromLocalLevelsChoice) gameObject.SetActive(false);
        else locallySavedLevelsPath = GeneralManager.locallySavedLevelsPath;
    }

    public void EraseLocallySavedLevel()
    {
        string levelName = GetComponentInParent<WindowConstructor>().levelName;
        File.Delete(locallySavedLevelsPath + levelName + ".txt");
        Destroy(transform.parent.gameObject);
    }
}
