using UnityEngine;

public class CharactersBehaviour : MonoBehaviour
{
    GameObject gameManager_go;
    GameManager gameManager_script;
    MovementManager movementManager_script;

    public bool isDead;

    [SerializeField] GameObject redSquare;
    GameObject redSquare_instance = null;

    private void Start()
    {
        gameManager_go = GameObject.Find("Game Manager");
        gameManager_script = gameManager_go.GetComponent<GameManager>();
        movementManager_script = gameManager_go.GetComponent<MovementManager>();
    }

    public void CharacMove(float xMove, float zMove)
    {
        Debug.Log("move");

        transform.position = new Vector3(transform.position.x + xMove, transform.position.y, transform.position.z + zMove);
        DisplayInformationMessage.HideInfoPanel();

        if (redSquare_instance != null)
        {
            Destroy(redSquare_instance);
            redSquare_instance = null;
        }

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
        if (transform.parent.GetComponent<BoxDatas>().box.type == LevelBoardBoxType.Witch || transform.parent.GetComponent<BoxDatas>().box.type == LevelBoardBoxType.Player) transform.parent.GetComponent<BoxDatas>().box.type = LevelBoardBoxType.None;
        int objectIndex = (int)(transform.position.x + (characPos.z * GameManager.level.w));

        transform.parent = movementManager_script.board.GetChild(objectIndex).transform;

        Debug.Log("newbox parent");
    }

    public void Die(LevelBoardBoxType boxType)
    {
        int x = gameManager_script.ConvertPosToBoxes(transform.position).x + 1;
        int y = gameManager_script.ConvertPosToBoxes(transform.position).y + 1;

        GameManager.alreadyDied = true;

        DisplayInformationMessage.Message(transform.name + " dies on a " + boxType.ToString() + " on " + x + "-" + y + "\n");

        if (!GeneralManager.isInBuildMode) gameManager_script.LevelLost();

        if (redSquare_instance == null)
        {
            redSquare_instance = Instantiate(redSquare);
            redSquare_instance.name = "Red Square";
            redSquare_instance.transform.position = new Vector3(transform.parent.position.x, .02f, transform.parent.position.z);
        }
    }
}
