using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class SaveLevelToDataBase : MonoBehaviour
{
    [SerializeField] bool local;
    [SerializeField] string url;
    [SerializeField] string urlLocal;
    [SerializeField] string addLevel;

    [SerializeField] GameObject waitingPanel, alertMessage;

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
            //Debug.Log("wating panel go : " + waitingPanel);
            ////GameObject waitingPanelInstance = Instantiate(waitingPanel, new Vector3(806,453.5f, 0), Quaternion.Euler(Vector3.zero), GameObject.Find("Canvas").transform);
            ////Screen.width * 0.5f, Screen.height * 0.5f  Quaternion.Euler(Vector3.zero), 
            //GameObject waitingPanelInstance = Instantiate(waitingPanel, GameObject.Find("Canvas").transform);
            //waitingPanelInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);
            //Debug.Log("wating panel instance : " + waitingPanel);
            //Debug.Log("wating panel instance name: " + waitingPanel.name);
            //Debug.Log("wating panel instance parent : " + waitingPanel.transform.parent);
            //Debug.Log("wating panel instance position : " + waitingPanel.transform.position);
            //Debug.Log("wating panel instance parent name: " + waitingPanel.transform.parent.name);
            //Debug.Log("canvas : " + GameObject.Find("Canvas"));
            //Debug.Log("canvas transform : " + GameObject.Find("Canvas").transform);
            //Debug.Log("canvas transform name : " + GameObject.Find("Canvas").transform.name);

            waitingPanel.SetActive(true);
            yield return www.SendWebRequest();
            waitingPanel.SetActive(false);

            //Destroy(waitingPanelInstance);
            alertMessage.SetActive(true);

            if (www.result != UnityWebRequest.Result.Success)
            {
                alertMessage.GetComponentInChildren<TextMeshProUGUI>().text = "Error: " + www.error;
                //DisplayAlertMessages.DisplayMessage("Error: " + www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                LevelError response = JsonUtility.FromJson<LevelError>(responseText);

                if (!response.success)
                {
                    alertMessage.GetComponentInChildren<TextMeshProUGUI>().text = response.error.message;
                }
                else
                {
                    alertMessage.GetComponentInChildren<TextMeshProUGUI>().text = "Level recorded";
                }
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
