using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script qui permet de vérifier si les valeurs entrées pour le cycle de vie de chaque objet sont cohérentes
/// </summary>
public class CustomValuesChecker : MonoBehaviour
{
    [SerializeField] GameObject alertMessage;

    public void VerifyCustomValues()
    {
        GameObject buildTourValueInput = GameObject.Find("Build_InputValue");
        GameObject destroyTurnValue = GameObject.Find("Destroy_InputValue");

        string s_buildTurnValue = buildTourValueInput.GetComponent<Text>().text;
        string s_destroyTurnValue = destroyTurnValue.GetComponent<Text>().text;


        //Vérification valeur destruction objet
        int i_destroyTurnValue;

        if (string.Equals(s_destroyTurnValue, "") || string.Equals(s_destroyTurnValue, "0")) i_destroyTurnValue = 0;
        else i_destroyTurnValue = int.Parse(s_destroyTurnValue);


        //Vérification valeur construction objet
        int i_buildTourValue;

        if (string.Equals(s_buildTurnValue, "") || string.Equals(s_buildTurnValue, "0")) i_buildTourValue = 0;
        else i_buildTourValue = int.Parse(s_buildTurnValue);


        if (i_buildTourValue > i_destroyTurnValue && (i_destroyTurnValue != 0))
        {
            alertMessage.SetActive(true);
            alertMessage.GetComponentInChildren<TextMeshProUGUI>().text = "The number of the turn in which the object is to appear is greater than the turn in which it is to disappear.";
        }

        GameObject goToModify = GameObject.Find("CustomisationPanel").GetComponent<LinkedGameObject>().linkedGameObject;
        if (goToModify) SetObjectCustomValues(goToModify, i_buildTourValue, i_destroyTurnValue);

        HidePanel("CustomCreation_Panel");

        buildTourValueInput.transform.parent.GetComponent<InputField>().text = "";
        destroyTurnValue.transform.parent.GetComponent<InputField>().text = "";


    }

    public void VerifyBlinkingFrequencyValue(GameObject blinkingFrequency_InputField)
    {
        if (blinkingFrequency_InputField.GetComponent<Text>().text != "")
        {
            int i_blinkingFrequency = int.Parse(blinkingFrequency_InputField.GetComponent<Text>().text);
            if (i_blinkingFrequency <= 1)
            {
                alertMessage.SetActive(true);
                alertMessage.GetComponentInChildren<TextMeshProUGUI>().text = "The blinking frequency may not be less than 2.";
            }
            else
            {
                GameObject goToModify = GameObject.Find("CustomisationPanel").GetComponent<LinkedGameObject>().linkedGameObject;
                if (goToModify) SetObjectBlinkingValues(goToModify, i_blinkingFrequency);

                blinkingFrequency_InputField.transform.parent.GetComponent<InputField>().text = "";
                HidePanel("DefineBlinking_Panel");
            }
        }
    }

    void SetObjectBlinkingValues(GameObject a_go, int frequency)
    {
        ObjectBlinking objectBlinking_Script = a_go.GetComponent<ObjectBlinking>();
        LevelBoardBox thisBox = a_go.transform.parent.GetComponent<BoxDatas>().box;


        if (!objectBlinking_Script)
        {
            Debug.LogWarning("Script ObjectBlinking n'est pas attaché à ce gameobject");
        }
        else
        {
            GameObject dropdown_go = GameObject.Find("Dropdown_BlinkingMode");

            int dropdownValue = dropdown_go.GetComponent<Dropdown>().value;//Défini le mode d'apparition de l'objet

            objectBlinking_Script.enabled = true;
            thisBox.blinkingMode = dropdownValue;
            thisBox.blinkingFrq = frequency;
        }
    }

    void SetObjectCustomValues(GameObject a_go, int a_creationTurn, int a_destructionMove)
    {
        CustomizeObjectLife customizeObjectLife_Script = a_go.GetComponent<CustomizeObjectLife>();
        LevelBoardBox thisBox = a_go.transform.parent.GetComponent<BoxDatas>().box;

        if (!customizeObjectLife_Script)
        {
            Debug.LogWarning("Script CustomizeObjectLife n'est pas attaché à ce gameobject");
        }
        else
        {
            customizeObjectLife_Script.enabled = true;
            thisBox.buildTurn = a_creationTurn;
            thisBox.destroyTurn = a_destructionMove;
        }
    }

    void HidePanel(string panelName) { GameObject.Find(panelName).SetActive(false); }
}
