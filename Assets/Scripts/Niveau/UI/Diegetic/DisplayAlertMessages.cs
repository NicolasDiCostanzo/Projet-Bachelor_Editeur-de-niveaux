﻿using TMPro;
using UnityEngine;

public class DisplayAlertMessages : MonoBehaviour
{
    [SerializeField] public GameObject errorMessagePanel;
    public static GameObject staticErrorMessagePanel;

    private void Start() {
        if (errorMessagePanel) staticErrorMessagePanel = errorMessagePanel;
        else                   enabled = false;
    }

    public static void DisplayMessage(string message)
    {
        if (staticErrorMessagePanel)
        {
            GameObject messagePanel = Instantiate(staticErrorMessagePanel);
            messagePanel.transform.SetParent(GameObject.Find("Canvas").transform);
            messagePanel.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
            messagePanel.GetComponentInChildren<TextMeshProUGUI>().text = message;
        }
        else
        {
            Debug.Log("Pas de panel d'erreurs. Erreur : " + message);
        }
    }
}