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
        Debug.Log("find object of type : " + FindObjectOfType<UI_Manager>());

        UI_Manager uiManager_script = GameObject.Find("Canvas").GetComponent<UI_Manager>();//FindObjectOfType<UI_Manager>();
        Debug.Log("ui manager script : " + uiManager_script);
        UI_Manager.selectedObject = typeAssociated;
        uiManager_script.ButtonOutlineManagement();

        Debug.Log("outline : " + GetComponent<Outline>());
        GetComponent<Outline>().enabled = true;

        Debug.Log("pliiiiiiiiiiiz");

    }

    GameObject instanceOfPrefab;

    public void DisplayInformation(bool a_displayInformation)
    {

        if (a_displayInformation)
        {
            Toggle toggle = GameObject.Find("Description boutons toggle").GetComponent<Toggle>();

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
