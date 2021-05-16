using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject[] objects = new GameObject[7];
    public int selectedObject;
    GameManager _GM;

    private void OnEnable() { 
        _GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        GameManager.RepositionBothCharacters();

        //PEUT Y AVOIR UN BUG ICIIIIIIIII
        if(GeneralManager.sceneNameToLoad != "") SaveLoadLevelData.LoadFromSavedLevelsDirectory(GeneralManager.sceneNameToLoad);
    }


    public GameObject CreateObject(LevelBoardBoxType boxType, Transform box)
    {
        if ((box.childCount <= 0) && (boxType != LevelBoardBoxType.None))
        {
            GameObject goToCreate = objects[(int)boxType];
            GameObject newGo = Instantiate(goToCreate, box.position, box.rotation);
            newGo.name = boxType.ToString();

            newGo.transform.parent = box;

            box.GetComponent<BoxDatas>().box.type = boxType;

            if (boxType == LevelBoardBoxType.Player)
            {
                GameManager.player = newGo;
                GameManager.playerIndex = box.GetSiblingIndex();
            }
            else if (boxType == LevelBoardBoxType.Witch)
            {
                GameManager.witch = newGo;
                GameManager.witchIndex = box.GetSiblingIndex();
            }

            return newGo;
        }

        return null;
    }

    public void RemoveObjectsFromBox(Transform boxTransform)
    {
        for (int i = 0; i < boxTransform.childCount; i++)
        {
            GameObject objectGO = boxTransform.GetChild(i).gameObject;
            DeactiveObject(objectGO);
            Destroy(objectGO);
        }
    }

    public void EraseBoxDatas(LevelBoardBox box)
    {
        box.blinkingFrq = 0;
        box.blinkingMode = 0;
        box.buildTurn = 0;
        box.destroyTurn = 0;
        box.type = LevelBoardBoxType.None;
    }

    public void ActiveObject(GameObject go)
    {
        GameObject parentBox = go.transform.parent.gameObject;

        if (parentBox.GetComponent<BoxesScript>())
        {
            int boxIndex = parentBox.GetComponent<BoxesScript>().boxIndex;
            LevelBoardBoxType thisBoxType = go.GetComponent<ObjectManager>().type;
            GameManager.level.boxes[boxIndex].type = thisBoxType;
            go.GetComponent<Renderer>().enabled = true;
        }
    }

    public void DeactiveObject(GameObject go)
    {
        GameObject parentBox = go.transform.parent.gameObject;

        if (parentBox.GetComponent<BoxesScript>())
        {
            int boxIndex = parentBox.GetComponent<BoxesScript>().boxIndex;
            GameManager.level.boxes[boxIndex].type = LevelBoardBoxType.None;
            go.GetComponent<Renderer>().enabled = false;
        }
    }
}