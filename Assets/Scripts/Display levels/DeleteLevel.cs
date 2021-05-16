using System.Collections;
using System.IO;
using UnityEngine;

public class DeleteLevel : MonoBehaviour
{
    string locallySavedLevelsPath;
    private void Start()
    {
        if (GeneralManager.isComingFromLocalLevelsChoice)
        {
            locallySavedLevelsPath = GeneralManager.locallySavedLevelsPath;
        }
        //else
        //{
        //    Debug.Log(GetComponentInParent<WindowConstructor>().level.creatorName);
        //    Debug.Log(System.Environment.UserName);
        //    if (GetComponentInParent<WindowConstructor>().level.creatorName != System.Environment.UserName) Destroy(gameObject);
        //}
    }

    public void DeleteLevel_PublicTransport()
    {
        StartCoroutine(f_DeleteLevel());
    }

    public IEnumerator f_DeleteLevel()
    {
        string levelName = GetComponentInParent<WindowConstructor>().levelName;

        if (!GeneralManager.isComingFromLocalLevelsChoice)
            yield return StartCoroutine(GameObject.Find("Game Manager").GetComponent<DataBaseRequest>().DeleteLevel(levelName));
        else
        {
            File.Delete(locallySavedLevelsPath + levelName + ".txt");
            Debug.Log(locallySavedLevelsPath + levelName + ".txt");
            Debug.Log("delete file");
        }

        Destroy(transform.parent.gameObject);
    }
}
