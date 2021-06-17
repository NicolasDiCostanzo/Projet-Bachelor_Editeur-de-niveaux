using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class SaveLevelToDataBase : MonoBehaviour
{
    [SerializeField] bool local;
    [SerializeField] string url;
    [SerializeField] string urlLocal;
    [SerializeField] string addLevel;

    [SerializeField] GameObject waitingPanel;

    public void SaveData() { StartCoroutine(SendLevelToDatabase()); }

    public IEnumerator SendLevelToDatabase()
    {
        if (local) url = urlLocal;

        Level level = SaveLoadLevelData.levelToSave;

        WWWForm form = new WWWForm();

        int i_nightLevel;

        if (SaveLoadLevelData.levelToSave.isInDarkMode) i_nightLevel = 1;
        else i_nightLevel = 0;

        form.AddField("levelName", level.levelName);
        form.AddField("creationDate", level.creationDate);
        form.AddField("levelDataJson", SaveLoadLevelData.levelToSave_json);
        form.AddField("maxTurns", level.nbTurns);
        form.AddField("traps", level.objectsContained);
        form.AddField("nightLevel", i_nightLevel);

        using (UnityWebRequest www = UnityWebRequest.Post(url + addLevel, form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            Debug.Log("wating panel go : " + waitingPanel);
            //GameObject waitingPanelInstance = Instantiate(waitingPanel, new Vector3(806,453.5f, 0), Quaternion.Euler(Vector3.zero), GameObject.Find("Canvas").transform);
            GameObject waitingPanelInstance = Instantiate(waitingPanel, new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0), Quaternion.Euler(Vector3.zero));
            waitingPanelInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);

            Debug.Log("wating panel instance : " + waitingPanelInstance);
            Debug.Log("wating panel instance name: " + waitingPanelInstance.name);
            Debug.Log("wating panel instance parent : " + waitingPanelInstance.transform.parent);
            Debug.Log("wating panel instance position : " + waitingPanelInstance.transform.position);
            Debug.Log("wating panel instance parent name: " + waitingPanelInstance.transform.parent.name);
            Debug.Log("canvas : " + GameObject.Find("Canvas"));
            Debug.Log("canvas transform : " + GameObject.Find("Canvas").transform);
            Debug.Log("canvas transform name : " + GameObject.Find("Canvas").transform.name);

            yield return www.SendWebRequest();

            Destroy(waitingPanelInstance);

            if (www.result != UnityWebRequest.Result.Success)
            {
                DisplayAlertMessages.DisplayMessage("Error: " + www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                LevelError response = JsonUtility.FromJson<LevelError>(responseText);

                if (!response.success)
                    DisplayAlertMessages.DisplayMessage(response.error.message);
                else
                    DisplayAlertMessages.DisplayMessage("Niveau enregistré ! :D");

            }
        }

        SaveLevelLocally(SaveLoadLevelData.levelToSave_json);
    }

    void SaveLevelLocally(string json)
    {
        string directory = Application.persistentDataPath + SaveLoadLevelData.directoryDownloadedLevels;

        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        Level level = JsonUtility.FromJson<Level>(json);
        level.completed = true;

        string levelName = level.levelName;
        json = JsonUtility.ToJson(level, true);

        File.WriteAllText(directory + levelName + ".txt", json);
    }
}
