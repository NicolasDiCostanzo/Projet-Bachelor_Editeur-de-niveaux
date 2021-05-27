using UnityEngine;

public class BoxDatas : MonoBehaviour
{
    public LevelBoardBox box;

    public int index;

    void Start()
    {
        int boxIndex = transform.GetSiblingIndex();

        //Debug.Log(GameManager.level);
        Debug.Log(GameManager.level.boxes.Count);
        box = GameManager.level.boxes[boxIndex];
        index = transform.GetSiblingIndex();
    }

    public void SaveDatasInLevelBoard()
    {
        GameManager.level.boxes[index] = box;
    }
}
