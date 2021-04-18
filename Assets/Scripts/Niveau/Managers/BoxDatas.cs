using UnityEngine;

public class BoxDatas : MonoBehaviour
{
    public LevelBoardBox box;

    GameObject _GM_GO;
    GameManager _GM;
    public int index;

    void Start()
    {
        int boxIndex = transform.GetSiblingIndex();

        _GM_GO = GameObject.Find("Game Manager");
        box = GameManager.level.boxes[boxIndex];
        _GM = _GM_GO.GetComponent<GameManager>();
        index = transform.GetSiblingIndex();
    }

    public void SaveDatasInLevelBoard()
    {
        GameManager.level.boxes[index] = box;
    }
}
