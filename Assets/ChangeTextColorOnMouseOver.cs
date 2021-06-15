using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeTextColorOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Color startingColor;
    [SerializeField] Color overColor;

    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        startingColor = text.color;
    }

    void OnEnable() { text.color = startingColor; }

    public void OnPointerEnter(PointerEventData eventData) { text.color = overColor; }

    public void OnPointerExit(PointerEventData eventData) { text.color = startingColor; }
}
