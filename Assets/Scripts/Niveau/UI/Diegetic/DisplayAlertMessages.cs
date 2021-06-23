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


    public /*static*/ void Deactive()
    {
        staticErrorMessagePanel.SetActive(false);
    }

    public /*static*/ void DisplayMessage(string message)
    {

        if (staticErrorMessagePanel)
        {
            staticErrorMessagePanel.SetActive(true);

            //GameObject messagePanel = Instantiate(staticErrorMessagePanel);
            //staticErrorMessagePanel.transform.SetParent(GameObject.Find("Canvas").transform);

            //RectTransform rectTransform = staticErrorMessagePanel.GetComponent<RectTransform>();

            //rectTransform.offsetMin = Vector2.zero;
            //rectTransform.offsetMax = Vector2.zero;
            //rectTransform.localScale = Vector2.one;
            staticErrorMessagePanel.GetComponentInChildren<TextMeshProUGUI>().text = message;

            GameManager.canBuild = false;
        }
        else
        {
            Debug.Log("Pas de panel d'erreurs. Erreur : " + message);
        }
    }
}
