using UnityEngine;

public class PlaceWayOut : MonoBehaviour
{
    [HideInInspector] public GameObject assimilatedWayOut;

    void Start() {
        if(!GameObject.Find("Teleport_OUT")) UI_Manager.selectedObject = LevelBoardBoxType.Teleport_OUT;
    }

    void OnDestroy()
    {
        FindObjectOfType<BuildManager>().EraseBoxDatas(assimilatedWayOut.transform.parent.GetComponent<BoxDatas>().box);
        Destroy(assimilatedWayOut.gameObject);
        assimilatedWayOut = null;
    }
}
