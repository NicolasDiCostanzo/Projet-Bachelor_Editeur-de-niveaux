using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BuildButton : MonoBehaviour
{
    //public bool canBeInstanciedSeveralTimes;
    public LevelBoardBoxType typeAssociated;

    [SerializeField] GameObject informationPanel;
    [SerializeField] int informationPanelMargin;

    [TextArea(5, 20)]
    [SerializeField] string mouseOverText;

    public void SetSelectedObject()
    {
        UI_Manager uiManager_script = GameObject.Find("Canvas").GetComponent<UI_Manager>();

        UI_Manager.selectedObject = typeAssociated;
        uiManager_script.ButtonOutlineManagement();
        GetComponent<Outline>().enabled = true;
    }

    GameObject instanceOfPrefab;

    public void DisplayInformation(bool a_displayInformation)
    {

        if (a_displayInformation)
        {
            Toggle toggle = GameObject.Find("Toggle").GetComponent<Toggle>();

            if (toggle.isOn)
            {
                informationPanel.GetComponentInChildren<TextMeshProUGUI>().text = mouseOverText;
                instanceOfPrefab = Instantiate(informationPanel, transform);

                instanceOfPrefab.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
            }
        }
        else
        {
            if (instanceOfPrefab) Destroy(instanceOfPrefab);
        }
    }


}
