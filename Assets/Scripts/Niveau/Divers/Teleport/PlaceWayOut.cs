using UnityEngine;

public class PlaceWayOut : MonoBehaviour
{
    [HideInInspector] public GameObject assimilatedWayOut;

    void Start() { UI_Manager.selectedObject = LevelBoardBoxType.Teleport_OUT; ; }

    void OnDestroy()
    {
        Destroy(assimilatedWayOut.gameObject);
        assimilatedWayOut = null;
    }
}
