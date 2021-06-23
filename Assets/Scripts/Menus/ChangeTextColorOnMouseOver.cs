using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeTextColorOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Color startingColor;
    [SerializeField] Color overColor, colorIfNotInteractable;

    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        startingColor = text.color;
    }

    void OnEnable()
    {
        DetermineColorOnStart();
    }

    public void DetermineColorOnStart()
    {
        if (!transform.parent.GetComponent<UnityEngine.UI.Button>().IsInteractable())
        {
            text.color = colorIfNotInteractable;
        }
        else
        {
            text.color = startingColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) { if(transform.parent.GetComponent<UnityEngine.UI.Button>().IsInteractable()) text.color = overColor; }

    public void OnPointerExit(PointerEventData eventData) { if (transform.parent.GetComponent<UnityEngine.UI.Button>().IsInteractable()) text.color = startingColor; }
}
