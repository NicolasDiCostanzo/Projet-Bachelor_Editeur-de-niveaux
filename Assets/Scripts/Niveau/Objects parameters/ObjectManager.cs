using UnityEngine;

/// <summary>
///Définit des paramètres et des comportements à la création et déstruction de l'objet
/// </summary>
public class ObjectManager : MonoBehaviour
{
    public Vector3 positionOffset;
    public LevelBoardBoxType type;
    UI_Manager uiManagerReference;

    public bool canBeInstantiatedSeveralTimes;
    GameObject teleportIN;
    GameObject teleportOut;

    private void Awake()
    {
        uiManagerReference = FindObjectOfType<UI_Manager>();
        positionOffset.y = transform.localScale.y;

        if (type == LevelBoardBoxType.Tree || type == LevelBoardBoxType.Trap) positionOffset.y = transform.localScale.y / 2;
    }

    private void OnEnable()
    {
        transform.name = type.ToString();
        transform.position += positionOffset;

        if (!canBeInstantiatedSeveralTimes)
        {
            uiManagerReference.ButtonManagement(type, false);
            UI_Manager.selectedObject = LevelBoardBoxType.None;
            uiManagerReference.ButtonOutlineManagement();
        }

        ManageTeleportsExceptionOnCreation();
    }

    private void OnDestroy()
    {
        if (!canBeInstantiatedSeveralTimes) uiManagerReference.ButtonManagement(type, true);
        ManageTeleportsExceptionOnDestroy();
        uiManagerReference.ButtonOutlineManagement();
    }

    void ManageTeleportsExceptionOnCreation()
    {
        teleportIN = GameObject.Find("Teleport_IN");
        teleportOut = GameObject.Find("Teleport_OUT");

        if (teleportIN && !teleportOut) UI_Manager.SetTPTextDisplaying(true);
        else UI_Manager.SetTPTextDisplaying(false);

        if (type == LevelBoardBoxType.Teleport_IN) uiManagerReference.ButtonsAreDisabled(true);
        if (type == LevelBoardBoxType.Teleport_OUT) uiManagerReference.ButtonsAreDisabled(false);
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

        if (type == LevelBoardBoxType.Teleport_OUT && teleportIN) uiManagerReference.ButtonsAreDisabled(true);
        else if (type == LevelBoardBoxType.Teleport_IN) uiManagerReference.ButtonsAreDisabled(false);
    }
}
