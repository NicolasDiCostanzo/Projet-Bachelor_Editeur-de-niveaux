using TMPro;
using UnityEngine;
public class DisplayingOfObjectLifeDatas : MonoBehaviour
{
    bool objectLifeIsCustomed;
    GameObject dataDisplayer;

    [SerializeField] float timeBeforeDisplaying;
    float countBeforeDisplay;

    [SerializeField] Vector3 panelPosOffset = Vector3.zero;

    bool mouseInOn;


    void Start()
    {
        dataDisplayer = GameObject.Find("PanelGO");
        countBeforeDisplay = timeBeforeDisplaying;
    }

    void Update()
    {
        objectLifeIsCustomed = GetComponent<ObjectBlinking>().enabled || GetComponent<CustomizeObjectLife>().enabled;

        if (mouseInOn) timeBeforeDisplaying -= Time.deltaTime;

        if (timeBeforeDisplaying <= 0) DisplayPanel();
    }



    private void OnMouseEnter() { mouseInOn = true; }

    private void OnMouseExit() { HidePanel(); }

    private void OnDestroy() { HidePanel(); }
    void DisplayPanel()
    {
        if (objectLifeIsCustomed) dataDisplayer.transform.position = transform.position + panelPosOffset;

        if (GetComponent<ObjectBlinking>().enabled) dataDisplayer.GetComponentInChildren<TextMeshProUGUI>().text = BlinkingText();
        else dataDisplayer.GetComponentInChildren<TextMeshProUGUI>().text = LimitedLifeText();
    }

    void HidePanel()
    {
        mouseInOn = false;
        timeBeforeDisplaying = countBeforeDisplay;
        if (objectLifeIsCustomed && dataDisplayer != null) dataDisplayer.transform.localPosition = Vector3.zero;
    }

    string BlinkingText()
    {
        LevelBoardBox thisBox = transform.parent.GetComponent<BoxDatas>().box;

        int blinkingMode = thisBox.blinkingMode;
        int frq = thisBox.blinkingFrq;
        string blinkingModeText;

        if (blinkingMode == 0) blinkingModeText = "<b>A</b>ppear";
        else blinkingModeText = "<b>Dis</b>appear";

        return blinkingModeText + " every <b>" + frq + "</b> turn"; ;
    }

    string LimitedLifeText()
    {
        LevelBoardBox thisBox = transform.parent.GetComponent<BoxDatas>().box;

        int creationTurn = thisBox.buildTurn;
        int destructionTurn = thisBox.destroyTurn;

        if(destructionTurn > creationTurn) return "Building turn " + creationTurn + "\nDestroying turn " + destructionTurn;
        else                               return "Build turn " + creationTurn + "\nNot destroyed";

    }
}
