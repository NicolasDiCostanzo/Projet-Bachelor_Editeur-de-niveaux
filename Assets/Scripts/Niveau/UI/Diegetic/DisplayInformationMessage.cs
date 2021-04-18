using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
// Script qui gère l'affichage des informations pendant que le joueur teste son niveau. Par exemple, si le paysan ou la sorcière sont sur un piège, si le niveau a été réussi etc...
/// </summary>
public class DisplayInformationMessage : MonoBehaviour
{
    public static Image img;
    public static TextMeshProUGUI textMP;

    void OnEnable()
    {
        img = GetComponent<Image>();
        textMP = GetComponentInChildren<TextMeshProUGUI>();
    }

    public static void Message(string a_message)
    {
        if (!img.isActiveAndEnabled) img.enabled = true;

        if (!textMP.isActiveAndEnabled) textMP.enabled = true;
        if(textMP.text != a_message) textMP.text += a_message;
    }

    public static void HideInfoPanel()
    {
        if (textMP)
        {
            textMP.text = "";
            textMP.enabled = false;
        }

        if(img) img.enabled = false;
    }
}
