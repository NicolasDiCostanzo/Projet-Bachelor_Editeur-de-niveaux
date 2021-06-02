using UnityEngine;
using UnityEngine.UI;

public class CustomizeObjectLife : MonoBehaviour
{
    bool isActive;

    BuildManager _BM;
    MovementManager _MM;
    GameManager _GM;


    Renderer rend;
    [HideInInspector] public Color originial_go_color;

    BoxDatas thisBox;

    private void OnEnable()
    {
        GameObject gameManagerGO = GameObject.Find("Game Manager");
        _BM = gameManagerGO.GetComponent<BuildManager>();
        _MM = gameManagerGO.GetComponent<MovementManager>();
        _GM = gameManagerGO.GetComponent<GameManager>();

        rend = gameObject.GetComponent<Renderer>();
        originial_go_color = rend.material.color;

        if (GetComponent<ObjectBlinking>().enabled) GetComponent<ObjectBlinking>().enabled = false;

        GameObject.Find("Cancel_btn").GetComponent<Button>().interactable = true;

        thisBox = transform.parent.GetComponent<BoxDatas>();
    }

    void LateUpdate()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) 
            || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow)) 
            && GameManager.state == State.Play)
            ObjectLifeManagement();
    }

    public void ActiveObject()
    {
        if (!isActive)
        {
            _BM.ActiveObject(transform.gameObject);
            isActive = true;
        }
    }

    void DeactiveObject()
    {
        if (isActive)
        {
            _BM.DeactiveObject(transform.gameObject);
            isActive = false;
        }
    }

    public void ObjectLifeManagement()
    {
        if (GameManager.state == State.Play)
        {
            if (GameManager.currentTurn == 0 && thisBox.box.buildTurn > 0)
            {
                isActive = true;
                DeactiveObject();
                return;
            }

            if (GameManager.currentTurn == thisBox.box.buildTurn) ActiveObject();
            else if (GameManager.currentTurn == thisBox.box.destroyTurn && thisBox.box.destroyTurn > thisBox.box.buildTurn) DeactiveObject();
        }
        else
        {
            ActiveObject();
        }

    }
}
