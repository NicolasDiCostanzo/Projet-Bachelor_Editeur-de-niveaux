using UnityEngine;

public class Link_TpIN_to_TpOUT : MonoBehaviour
{
    GameObject assimilatedWayIn;

    void Start()
    {
        GameObject tp_in = GameObject.Find("Teleport_IN");
        PlaceWayOut placeWayOut_script = tp_in.GetComponent<PlaceWayOut>();

        assimilatedWayIn = tp_in;
        placeWayOut_script.assimilatedWayOut = gameObject;
    }

    void OnDestroy()
    {
        //Seulement si le portail de sortie a été détruit...
        if (assimilatedWayIn) UI_Manager.selectedObject = LevelBoardBoxType.Teleport_OUT;
    }
}
