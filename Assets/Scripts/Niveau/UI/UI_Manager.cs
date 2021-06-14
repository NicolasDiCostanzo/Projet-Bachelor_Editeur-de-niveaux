using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static LevelBoardBoxType selectedObject = LevelBoardBoxType.None;

    [SerializeField] Transform buttonsParent;
    [SerializeField] GameObject tpText;
    public static GameObject _tpText;
    public RawImage deactiveButtonsImg;

    public List<GameObject> isNotActiveWhenNotInEditor = new List<GameObject>();

    private void Start()
    {
        _tpText = tpText;
        selectedObject = LevelBoardBoxType.None;
    }

    public void DisplayPanel(GameObject panelToDisplay) { if (!panelToDisplay.activeInHierarchy) panelToDisplay.SetActive(true); }

    public void HidePanel(GameObject panelToHide) { if (panelToHide.activeInHierarchy) panelToHide.SetActive(false); }

    public List<Button> buttonAlreadyDeactivated = new List<Button>();
    public void ButtonManagement(LevelBoardBoxType objectType, bool buttonIsInteractable)
    {
        if (objectType == LevelBoardBoxType.Teleport_IN) buttonAlreadyDeactivated.Clear();

        if (buttonsParent)
        {
            for (int i = 0; i < buttonsParent.childCount; i++)
            {
                GameObject buttonGO = buttonsParent.GetChild(i).gameObject;

                if (buttonGO.GetComponent<BuildButton>().typeAssociated == objectType) buttonGO.GetComponent<Button>().interactable = buttonIsInteractable;
            }
        }

    }

    public static void SetTPTextDisplaying(bool displayText)
    {
        if(_tpText) if(_tpText.activeInHierarchy != displayText) _tpText.SetActive(displayText);
    }

    public void ButtonOutlineManagement()
    {
        if (buttonsParent)
        {
            for (int i = 0; i < buttonsParent.childCount; i++)
            {
                GameObject buttonGO = buttonsParent.GetChild(i).gameObject;
                Outline buttonOutline = buttonGO.GetComponent<Outline>();

                if (buttonGO.GetComponent<BuildButton>().typeAssociated != selectedObject) buttonOutline.enabled = false;
                else buttonOutline.enabled = true;
            }
        }
    }

    public void DeactiveNotAuthorizedFunctionalities() { 
        for (int i = 0; i < isNotActiveWhenNotInEditor.Count; i++) 
            isNotActiveWhenNotInEditor[i].SetActive(false); 
    }

    public void DeactiveButtons(bool active) { deactiveButtonsImg.raycastTarget = active; }
}
