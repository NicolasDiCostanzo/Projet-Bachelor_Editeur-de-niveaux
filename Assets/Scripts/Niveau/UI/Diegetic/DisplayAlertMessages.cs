using TMPro;
using UnityEngine;

public class DisplayAlertMessages : MonoBehaviour
{
    [SerializeField] public GameObject errorMessagePanel;
    public static GameObject staticErrorMessagePanel;

    private void Start()
    {
        if (errorMessagePanel) staticErrorMessagePanel = errorMessagePanel;
    }

    public static void DisplayMessage(string message)
    {

        if (staticErrorMessagePanel)
        {
            GameObject messagePanel = Instantiate(staticErrorMessagePanel);
            messagePanel.transform.SetParent(GameObject.Find("Canvas").transform);

            RectTransform rectTransform = messagePanel.GetComponent<RectTransform>();

            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.localScale = Vector2.one;
            messagePanel.GetComponentInChildren<TextMeshProUGUI>().text = message;

            GameManager.canBuild = false;
        }
        else
        {
            Debug.Log("Pas de panel d'erreurs. Erreur : " + message);
        }
    }
}
