using TMPro;
using UnityEngine;

public class DisplayAlertMessages : MonoBehaviour
{
    [SerializeField] public GameObject errorMessagePanel;
    public static GameObject staticErrorMessagePanel;

    private void Start() {
        if (errorMessagePanel) staticErrorMessagePanel = errorMessagePanel;
    }

    public static void DisplayMessage(string message)
    {
        if (staticErrorMessagePanel)
        {
            GameManager.SetCanBuild(false);

            GameObject messagePanel = Instantiate(staticErrorMessagePanel);
            messagePanel.transform.SetParent(GameObject.Find("Canvas").transform);

            RectTransform rect = messagePanel.GetComponent<RectTransform>();

            rect.position = Vector3.zero;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            rect.localScale = Vector3.one;
            messagePanel.GetComponentInChildren<TextMeshProUGUI>().text = message;
        }
        else
        {
            Debug.Log("Pas de panel d'erreurs. Erreur : " + message);
        }
    }
}
