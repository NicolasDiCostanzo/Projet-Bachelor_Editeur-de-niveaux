using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class GetLevelsDataFromDatabase : MonoBehaviour
{
    [SerializeField] bool testInLocal;
    [SerializeField] string url;
    [SerializeField] string urlLocal;
    [SerializeField] string json_phpFunctionName;
    [SerializeField] string trapsString_phpFunctionName;
    [SerializeField] string getLevelWithSpecificName_phpFunctionName;

    [SerializeField] GameObject filterByName;

    string[] jsons;

    private void Start() {
        if(testInLocal) url = urlLocal;

        if (!GeneralManager.isComingFromDatabaseLevelsChoice) GeneralManager.isComingFromDatabaseLevelsChoice = true;

        filterByName.SetActive(true);
        StartCoroutine(DisplayLevels()); 
    }

    private void OnDestroy() { if(filterByName) filterByName.SetActive(false); }

    public void DisplayLevelPublicTransport() { StartCoroutine(DisplayLevels()); }

    IEnumerator DisplayLevels()
    {  
        yield return StartCoroutine(GetJson());
        yield return StartCoroutine(GetTrapsString());

        GetComponent<DisplayLevelManager>().f_DisplayLevels();     
    }

    IEnumerator GetJson()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + json_phpFunctionName))
        {
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));

            UnityWebRequest aaa = UnityWebRequest.Post(url, formData);

            //aaa.chunkedTransfer = false;//ADD THIS LINE


            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError) Debug.LogWarning(www.error);

            if (www.isDone)
            {
                string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                jsons = jsonResult.Split(new string[] { "newLevel" }, StringSplitOptions.None);
                DisplayLevelManager.levelsToDisplay.Clear();

                for (int i = 0; i < jsons.Length; i++)
                {
                    Level newLevel = JsonUtility.FromJson<Level>(jsons[i]);
                    DisplayLevelManager.levelsToDisplay.Add(newLevel);
                }

                string responseText = www.downloadHandler.text;

                Debug.Log(responseText);
            }
        }
    }

    IEnumerator GetTrapsString()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + trapsString_phpFunctionName))
        {
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));

            UnityWebRequest aaa = UnityWebRequest.Post(url, formData);

            //aaa.chunkedTransfer = false;//ADD THIS LINE


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

        StartCoroutine(GetLevelWithSpecificName(pieceOfStringToFind));
    }

    IEnumerator GetLevelWithSpecificName(string a_stringToFindInName)
    {
        string get = url + getLevelWithSpecificName_phpFunctionName + "&string=" + a_stringToFindInName;

        using (UnityWebRequest www = UnityWebRequest.Get(get))
        {
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));

            UnityWebRequest aaa = UnityWebRequest.Post(url, formData);

            //aaa.chunkedTransfer = false;//ADD THIS LINE


            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError) Debug.LogWarning(www.error);

            if (www.isDone)
            {

                string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                jsons = jsonResult.Split(new string[] { "newLevel" }, StringSplitOptions.None);

                GetComponent<DisplayLevelManager>().EraseAllLevelsDisplayed();

                DisplayLevelManager.levelsToDisplay.Clear();


                for (int i = 0; i < jsons.Length; i++)
                {
                    Level newLevel = JsonUtility.FromJson<Level>(jsons[i]);
                    DisplayLevelManager.levelsToDisplay.Add(newLevel);
                }

                GetComponent<DisplayLevelManager>().f_DisplayLevels();

                //string responseText = www.downloadHandler.text;
                //Debug.Log(responseText);
            }
        }
    }
}
