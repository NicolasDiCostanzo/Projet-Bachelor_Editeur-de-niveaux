using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    GameObject player;
    GameObject witch;
    CharactersBehaviour playerBehavioursScript;
    CharactersBehaviour witchBehavioursScript;
    GameManager _GM;

    int xMove;
    int zMove;
    int BOX_SIZE;

    [SerializeField] TextMeshProUGUI nbOfMoves_Displayer;

    [HideInInspector] public Transform board;

    void OnEnable()
    {
        _GM = GetComponent<GameManager>();

        player = GameObject.Find("Player");
        if (player) playerBehavioursScript = player.GetComponent<CharactersBehaviour>();

        witch = GameObject.Find("Witch");
        if (witch) witchBehavioursScript = witch.GetComponent<CharactersBehaviour>();

        BOX_SIZE = GetComponent<GameManager>().BOX_SIZE;

        ResetNbOfMovesDisplayed();

        board = GameObject.Find("Squares").transform;
    }

    public void ResetNbOfMovesDisplayed()
    {
        nbOfMoves_Displayer.text = GameManager.currentTurn.ToString() + " / " + GameManager.nbOfMovesLimit;
        nbOfMoves_Displayer.color = Color.black;
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            GameManager.currentTurn++;

            nbOfMoves_Displayer.text = GameManager.currentTurn.ToString() + " / " + GameManager.nbOfMovesLimit;

            if ((GameManager.currentTurn > GameManager.nbOfMovesLimit) && GameManager.nbOfMovesLimit > 0)
            {
                if (nbOfMoves_Displayer.color != new Color32(255, 0, 0, 255)) nbOfMoves_Displayer.color = new Color32(255, 0, 0, 255);
                
                if(!GeneralManager.isInBuildMode) _GM.LevelLost();
            }
            else
            {
                if (nbOfMoves_Displayer.color != new Color32(0, 0, 0, 255)) nbOfMoves_Displayer.color = new Color32(0, 0, 0, 255);
            }

            for (int i = 0; i < GameManager.boxesParent.transform.childCount; i++)
            {
                GameObject box = GameManager.boxesParent.transform.GetChild(i).gameObject;

                if (box.transform.childCount > 0)
                {
                    Transform objOnBox = box.transform.GetChild(0);
                    if (objOnBox.GetComponent<ObjectBlinking>() && objOnBox.GetComponent<ObjectBlinking>().isActiveAndEnabled) objOnBox.GetComponent<ObjectBlinking>().DetermineState();

                    if (objOnBox.GetComponent<CustomizeObjectLife>() && objOnBox.GetComponent<CustomizeObjectLife>().isActiveAndEnabled) objOnBox.GetComponent<CustomizeObjectLife>().ObjectLifeManagement();

                }
            }
        }
    }

    void LateUpdate()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow)))
            Movement();
    }

    void Movement()
    {
        witch = GameObject.Find("Witch");
        player = GameObject.Find("Player");

        if (!player || !witch) return;

        if (player) playerBehavioursScript = player.GetComponent<CharactersBehaviour>();
        if (witch) witchBehavioursScript = witch.GetComponent<CharactersBehaviour>();

        //Initialisation des valeurs à 0
        xMove = 0;
        zMove = 0;

        if      (Input.GetKeyDown(KeyCode.UpArrow))    zMove = BOX_SIZE;  //FLECHE HAUT
        else if (Input.GetKeyDown(KeyCode.DownArrow))  zMove = -BOX_SIZE; //FLECHE BAS
        else if (Input.GetKeyDown(KeyCode.RightArrow)) xMove = BOX_SIZE;  //FLECHE DROITE
        else if (Input.GetKeyDown(KeyCode.LeftArrow))  xMove = -BOX_SIZE; //FLECHE GAUCHE


        Vector3 futurPlayerPos = new Vector3(player.transform.position.x + xMove, player.transform.position.y, player.transform.position.z + zMove);
        Vector3 futurWitchPos = new Vector3(witch.transform.position.x - xMove, witch.transform.position.y, witch.transform.position.z - zMove);

        (int x, int y) futurPlayerCoordonates = _GM.ConvertPosToBoxes(futurPlayerPos);
        (int x, int y) futurwitchCoordonates = _GM.ConvertPosToBoxes(futurWitchPos);

        if (EncounterObstacle(futurPlayerCoordonates).canMove) playerBehavioursScript.CharacMove(xMove, zMove);
        if (EncounterObstacle(futurwitchCoordonates).canMove) witchBehavioursScript.CharacMove(-xMove, -zMove);

        Vector3 playerPos = player.transform.position;
        Vector3 witchPos = witch.transform.position;

        (int x, int y) playerCoordonates = _GM.ConvertPosToBoxes(playerPos);
        (int x, int y) witchCoordonates = _GM.ConvertPosToBoxes(witchPos);

        if (EncounterObstacle(playerCoordonates).die) playerBehavioursScript.Die(LevelBoardBoxType.Trap);

        if (EncounterObstacle(witchCoordonates).die) witchBehavioursScript.Die(LevelBoardBoxType.Trap);

        if (IsOnTP(playerCoordonates, true))
        {
            playerPos = player.transform.position;
            playerCoordonates = _GM.ConvertPosToBoxes(playerPos);
        }

        if (IsOnTP(witchCoordonates, false))
        {
            witchPos = witch.transform.position;
            witchCoordonates = _GM.ConvertPosToBoxes(witchPos);
        }

        if (isOnTree(playerCoordonates)) playerBehavioursScript.Die(LevelBoardBoxType.Tree);

        if (GameManager.level.isInDarkMode)
            LightManagement.ToggleLight(isOnLamp(playerCoordonates));

        if (playerCoordonates == witchCoordonates) playerBehavioursScript.Die(LevelBoardBoxType.Witch);

        Debug.Log(GameManager.alreadyDied);

        if (IsOnBonfire(witchCoordonates) && !GameManager.alreadyDied) _GM.LevelCompleted();
        if (IsOnBonfire(playerCoordonates)) playerBehavioursScript.Die(LevelBoardBoxType.Fire);
    }

    /// <summary>
    /// Vérifications des obstacles (pièges, arbres et limites plateau)
    /// </summary>
    /// <param name="characCoordonates"></param>
    /// <returns></returns>
    (bool canMove, bool die) EncounterObstacle((int x, int y) characCoordonates)
    {
        bool canMove = true;
        bool die = false;

        //Vérification limites du plateau
        if (characCoordonates.x < 0 || characCoordonates.y < 0 || characCoordonates.x >= _GM.w || characCoordonates.y >= _GM.h)//Si le personnage sort du plateau
        {
            canMove = false;
            return (canMove, die);
        }

        //Vérification des arbres et pièges
        List<LevelBoardBox> objectsList = GameManager.level.boxes;
        int objectIndex = characCoordonates.y * GameManager.level.w + characCoordonates.x;
        
        LevelBoardBox box = objectsList[objectIndex];

        switch (box.type)
        {
            case LevelBoardBoxType.Tree: canMove = false; break;
            case LevelBoardBoxType.Trap: die = true; break;
        }

        return (canMove, die);
    }

    bool isOnTree((int x, int y) characCoordonates)
    {
        List<LevelBoardBox> objectsList = GameManager.level.boxes;
        int objectIndex = characCoordonates.y * GameManager.level.w + characCoordonates.x;

        LevelBoardBox box = objectsList[objectIndex];

        if (box.type == LevelBoardBoxType.Tree) return true;
        else return false;
    }

    bool IsOnTP((int x, int y) characCoordonates, bool isPlayer)
    {
        List<LevelBoardBox> objectsList = GameManager.level.boxes;
        int objectIndex = characCoordonates.y * GameManager.level.w + characCoordonates.x;

        LevelBoardBox box = objectsList[objectIndex];

        if (box.type == LevelBoardBoxType.Teleport_IN)
        {
            GameObject tp_out = board.GetChild(objectIndex).transform.GetChild(0).GetComponent<PlaceWayOut>().assimilatedWayOut;
            if (isPlayer) playerBehavioursScript.CharacTP(tp_out.transform.position);
            else          witchBehavioursScript.CharacTP(tp_out.transform.position);

            return true;
        };

        return false;
    }

    bool IsOnBonfire((int x, int y) characCoordonates)
    {
        List<LevelBoardBox> objectsList = GameManager.level.boxes;
        int objectIndex = characCoordonates.y * GameManager.level.w + characCoordonates.x;

        LevelBoardBox box = objectsList[objectIndex];

        if (box.type == LevelBoardBoxType.Fire) return true;

        return false;
    }

    bool isOnLamp((int x, int y) characCoordonates)
    {
        List<LevelBoardBox> objectsList = GameManager.level.boxes;
        int objectIndex = characCoordonates.y * GameManager.level.w + characCoordonates.x;

        LevelBoardBox box = objectsList[objectIndex];

        if (box.type == LevelBoardBoxType.Lamp) return true;
        else return false;
    }
}