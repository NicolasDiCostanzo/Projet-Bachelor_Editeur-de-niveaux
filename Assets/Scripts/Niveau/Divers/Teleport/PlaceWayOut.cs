using UnityEngine;

public class PlaceWayOut : MonoBehaviour
{
    [HideInInspector] public GameObject assimilatedWayOut;

    void Start() {
        if(!GameObject.Find("Teleport_OUT")) UI_Manager.selectedObject = LevelBoardBoxType.Teleport_OUT;
    }

    void OnDestroy()
    {
        LevelBoardBox tpOutBox = assimilatedWayOut.transform.parent.GetComponent<BoxDatas>().box;
        if (tpOutBox != null && FindObjectOfType<BuildManager>()) FindObjectOfType<BuildManager>().EraseBoxDatas(tpOutBox);
        Destroy(assimilatedWayOut.gameObject);
        assimilatedWayOut = null;
    }
}
