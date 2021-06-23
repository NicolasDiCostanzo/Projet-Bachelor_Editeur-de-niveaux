using UnityEngine;
using TMPro;
public class SwitchState : MonoBehaviour
{
    [SerializeField] PlayState testLevelState_script = null;
    [SerializeField] BuildLevelState buildLevelState_script = null;
    [SerializeField] GameObject alertMessage;
    public void F_SwitchState()
    {
        //Si on va de l'état de construction vers celui de test, on vérifie si le niveau est viable.
        if (GameManager.state == State.Build)
        {

            if (SaveLoadLevelData.levelToSave.levelName == null || SaveLoadLevelData.levelToSave.levelName == "")
            {
                alertMessage.SetActive(true);
                alertMessage.GetComponentInChildren<TextMeshProUGUI>().text = "Level has no name...";

                return;
            }

            //On vérifie si un player ET une witch sont présents. Si non, alors le joueur ne peut pas tester le niveau.
            bool playerIsOnBoard = GameObject.Find("Player");
            bool witchIsOnBoard = GameObject.Find("Witch");

            if (!playerIsOnBoard || !witchIsOnBoard)
            {
                alertMessage.SetActive(true);
                alertMessage.GetComponentInChildren<TextMeshProUGUI>().text = "A Pawn and/or an Enemy is missing to test this level.";
                return;
            }


            //On vérifie que le niveau n'est pas un portail d'entrée, mais pas de portail de sortie. Si c'est le cas, le joueur ne peut pas tester le niveau.
            bool levelHasPortalIn = GameObject.Find("Teleport_IN");
            bool levelHasPortalOut = GameObject.Find("Teleport_OUT");

            if (levelHasPortalIn && !levelHasPortalOut)
            {
                alertMessage.SetActive(true);
                alertMessage.GetComponentInChildren<TextMeshProUGUI>().text = "A teleportation portal entrance is installed, but its exit is missing.";
                return;
            }


            //On vérifie si le niveau a un bûcher. Si non, le joueur peut quand même tester son niveau. On affiche quand même un message pour le prévenir.
            bool levelHasBonfire = GameObject.Find("Fire");

            if (!levelHasBonfire)
            {

                alertMessage.SetActive(true);
                alertMessage.GetComponentInChildren<TextMeshProUGUI>().text = "Arrival is missing, so the level has no finish...";
                return;
            }


            //Enregistrement du nombre maximum de coups autorisés
            string maxMoves = GameObject.Find("maxMoves_nb").GetComponent<TMPro.TMP_InputField>().text;

            GameManager.alreadyDied = false;

            if (maxMoves != "") GameManager.nbOfMovesLimit = int.Parse(maxMoves);
            else                GameManager.nbOfMovesLimit = 0;

            EnableTestState();
        }
        else
        {
            EnableBuildState();
        }

        GameManager.currentTurn = 0;
    }

    public void EnableTestState() { testLevelState_script.enabled = true; }

    public void EnableBuildState() { buildLevelState_script.enabled = true; }
}
