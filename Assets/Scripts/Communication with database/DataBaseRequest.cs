using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DataBaseRequest : MonoBehaviour
{
    [SerializeField] bool testInLocal;
    [SerializeField] string url;
    [SerializeField] string urlLocal;
    [SerializeField] string json_phpFunctionName;
    [SerializeField] string trapsString_phpFunctionName;
    [SerializeField] string getLevelWithSpecificName_phpFunctionName;
    [SerializeField] string deleteLevel_phpFunctionName;
    [SerializeField] string filtering_phpFunctionName;

    [SerializeField] GameObject filterByName;

    private void Start()
    {
        if (testInLocal) url = urlLocal;

        if (!GeneralManager.isComingFromDatabaseLevelsChoice) GeneralManager.isComingFromDatabaseLevelsChoice = true;

        //filterByName.SetActive(true);
        DisplayAllLevels();
    }

    void OnDestroy() { if (filterByName) filterByName.SetActive(false); }

    public void DisplayAllLevels() { StartCoroutine(GetResponse(url + json_phpFunctionName)); }

    IEnumerator GetTrapsString()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + trapsString_phpFunctionName))
        {
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));

            UnityWebRequest aaa = UnityWebRequest.Post(url, formData);

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError) Debug.LogWarning(www.error);

            if (www.isDone)
            {
                string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                string[] jsons = jsonResult.Split(new string[] { "^^" }, StringSplitOptions.None);

                for (int i = 0; i < jsons.Length; i++)
                    DisplayLevelManager.jsons.Add(jsons[i]);
            }
        }
    }

    public void GetLevelWithSpecificNamePublicTransport()
    {
        string pieceOfStringToFind = GameObject.Find("Searched Word").GetComponent<Text>().text;

        GetLevelWithSpecificName(pieceOfStringToFind);
    }

    void GetLevelWithSpecificName(string a_stringToFindInName)
    {
        string get = url + getLevelWithSpecificName_phpFunctionName + "&levelName=" + a_stringToFindInName;

        StartCoroutine(GetResponse(get));

        Debug.Log(DisplayLevelManager.levelsToDisplay.Count);
    }


    IEnumerator GetResponse(string urlToGet)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(urlToGet))
        {
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));

            UnityWebRequest aaa = UnityWebRequest.Post(url, formData);

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError) Debug.LogWarning(www.error);

            if (www.isDone)
            {
                string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                JsonLevelsResponse response = JsonUtility.FromJson<JsonLevelsResponse>(jsonResult);

                DisplayLevelManager.levelsToDisplay.Clear();

                for (int i = 0; i < response.data.Count; i++)
                {
                    Level newLevel = response.data[i];
                    DisplayLevelManager.levelsToDisplay.Add(newLevel);
                }
            }
        }

        GetComponent<DisplayLevelManager>().EraseAllLevelsDisplayed();
        GetComponent<DisplayLevelManager>().f_DisplayLevels();
    }

    public IEnumerator DeleteLevel(string levelName)
    {
        Debug.Log(url + deleteLevel_phpFunctionName + "&levelName=" + levelName);

        using (UnityWebRequest www = UnityWebRequest.Get(url + deleteLevel_phpFunctionName + "&levelName=" + levelName))
        {
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));

            UnityWebRequest aaa = UnityWebRequest.Post(url, formData);

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError) Debug.LogWarning(www.error);

            if (www.isDone)
            {
                string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                Debug.Log(jsonResult);
            }
        }
    }

    public void FilteringPublicTransport() { StartCoroutine(Filtering()); }

    IEnumerator Filtering()
    {
        string query = GameObject.Find("Game Manager").GetComponent<QueryManager>().CreateQuery();

        Debug.Log(query);

        WWWForm form = new WWWForm();
        form.AddField("sqlQuery", query);

        using (UnityWebRequest www = UnityWebRequest.Post(url + filtering_phpFunctionName, form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                DisplayAlertMessages.DisplayMessage("Error: " + www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log(responseText);


                if (www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(jsonResult);

                    JsonLevelsResponse response = JsonUtility.FromJson<JsonLevelsResponse>(jsonResult);

                    DisplayLevelManager.levelsToDisplay.Clear();

                    for (int i = 0; i < response.data.Count; i++)
                    {
                        Level newLevel = response.data[i];
                        DisplayLevelManager.levelsToDisplay.Add(newLevel);

                    }
                }
            }


        }

        GetComponent<DisplayLevelManager>().EraseAllLevelsDisplayed();
        GetComponent<DisplayLevelManager>().f_DisplayLevels();
    }
}

class JsonLevelsResponse
{
    public bool success;
    public List<Level> data;
    public LevelError erreur;
}

[Serializable]
class LevelError
{
    public bool success;
    public Error error;
}

class Error
{
    public int code;
    public string message;
}