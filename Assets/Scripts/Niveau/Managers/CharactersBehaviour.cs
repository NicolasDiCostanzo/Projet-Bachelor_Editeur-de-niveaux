using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

public class CharactersBehaviour : MonoBehaviour
{
    GameObject gameManager_go;
    GameManager gameManager_script;
    MovementManager movementManager_script;
    //public (int x, int y) characterCoordinates;
    public bool isDead;

    private void Start()
    {
        gameManager_go = GameObject.Find("Game Manager");
        gameManager_script = gameManager_go.GetComponent<GameManager>();
        movementManager_script = gameManager_go.GetComponent<MovementManager>();
        //characterCoordinates = gameManager.ConvertPosToBoxes(transform.position);
    }

    public void CharacMove(float xMove, float zMove)
    {
        transform.position = new Vector3(transform.position.x + xMove, transform.position.y, transform.position.z + zMove);
        DisplayInformationMessage.HideInfoPanel();
        NewBoxParent();
    }

    public void CharacTP(Vector3 tp_out)
    {
        transform.position = tp_out;
        NewBoxParent();
    }

    /// <summary>
    /// Assigne un nouveau parent en fonction de la position du personnage
    /// </summary>
    void NewBoxParent()
    {
        Vector3 characPos = transform.position;
        if(transform.parent.GetComponent<BoxDatas>().box.type == LevelBoardBoxType.Witch || transform.parent.GetComponent<BoxDatas>().box.type == LevelBoardBoxType.Player) transform.parent.GetComponent<BoxDatas>().box.type = LevelBoardBoxType.None;
        int objectIndex = (int)(transform.position.x + (characPos.z * GameManager.level.w));

        transform.parent = movementManager_script.board.GetChild(objectIndex).transform;
        //transform.parent.GetComponent<BoxDatas>().box.type = LevelBoardBoxType.None;
    }

    public void Die(LevelBoardBoxType boxType)
    {
        int x = gameManager_script.ConvertPosToBoxes(transform.position).x + 1;
        int y = gameManager_script.ConvertPosToBoxes(transform.position).y + 1;

        GameManager.alreadyDied = true;

        DisplayInformationMessage.Message(transform.name + " dies on a " + boxType.ToString() + " on " + x + "-" + y + "\n");

        if (!GeneralManager.isInBuildMode) gameManager_script.LevelLost();
    }
}
