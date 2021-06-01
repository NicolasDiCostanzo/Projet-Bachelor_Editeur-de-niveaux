using UnityEngine;

/// <summary>
///Définit des paramètres et des comportements à la création et déstruction de l'objet
/// </summary>
public class ObjectManager : MonoBehaviour
{
    public Vector3 positionOffset;
    public LevelBoardBoxType type;
    UI_Manager UI_Manager;

    public bool canBeInstantiatedSeveralTimes;
    GameObject teleportIN;
    GameObject teleportOut;

    private void Awake()
    {
        UI_Manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        positionOffset.y = transform.localScale.y;

        if (type == LevelBoardBoxType.Tree || type == LevelBoardBoxType.Trap) positionOffset.y = transform.localScale.y / 2;
    }

    private void OnEnable()
    {
        transform.name = type.ToString();
        transform.position += positionOffset;

        if (!canBeInstantiatedSeveralTimes)
        {
            UI_Manager.ButtonManagement(type, false);
            UI_Manager.selectedObject = LevelBoardBoxType.None;
            UI_Manager.ButtonOutlineManagement();
        }

        ManageTeleportsExceptionOnCreation();
    }

    private void OnDestroy()
    {
        if (!canBeInstantiatedSeveralTimes) UI_Manager.ButtonManagement(type, true);
        ManageTeleportsExceptionOnDestroy();
        UI_Manager.ButtonOutlineManagement();
    }

    void ManageTeleportsExceptionOnCreation()
    {
        teleportIN = GameObject.Find("Teleport_IN");
        teleportOut = GameObject.Find("Teleport_OUT");

        if (teleportIN && !teleportOut) UI_Manager.SetTPTextDisplaying(true);
        else UI_Manager.SetTPTextDisplaying(false);

        if (type == LevelBoardBoxType.Teleport_IN) UI_Manager.DeactiveButtons(true);
        if (type == LevelBoardBoxType.Teleport_OUT) UI_Manager.DeactiveButtons(false);
    }

    void ManageTeleportsExceptionOnDestroy()
    {
        teleportIN = GameObject.Find("Teleport_IN");
        teleportOut = GameObject.Find("Teleport_OUT");

        if (type != LevelBoardBoxType.Teleport_OUT) UI_Manager.selectedObject = type;

        if (teleportIN && !teleportOut)
        {
            UI_Manager.selectedObject = LevelBoardBoxType.Teleport_OUT;
            UI_Manager.SetTPTextDisplaying(true);
        }
        else
        {
            UI_Manager.SetTPTextDisplaying(false);
        }

        //Si l'objet détruit un TP_OUT et qu'il n'y a pas de TP_IN sur le plateau, on garde en selection un TP_IN
        if (UI_Manager.selectedObject == LevelBoardBoxType.Teleport_OUT && !teleportIN) UI_Manager.selectedObject = LevelBoardBoxType.Teleport_IN;

        if (type == LevelBoardBoxType.Teleport_OUT && teleportIN) UI_Manager.DeactiveButtons(true);
        else if (type == LevelBoardBoxType.Teleport_IN) UI_Manager.DeactiveButtons(false);
    }
}
