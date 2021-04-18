using UnityEngine;
using UnityEngine.UI;

public class ObjectBlinking : MonoBehaviour
{
    GameObject gameManagerGO;
    BuildManager _BM;

    //[HideInInspector] public int frequency;

    /// <summary>
    /// 'blinkingMode = 0' signifie que l'objet va APPRAÎTRE tous les x tours (sera donc INVISIBLE entre ces tours là).
    /// 'blinkingMode = 1' signifie que l'objet va DISPARAÎTRE tous les x tours (sera donc VISIBLE entre ces tours là)
    /// </summary>
    //[HideInInspector] public int blinkingMode;

    Renderer rend;
    [HideInInspector] public Color original_go_color;

    BoxDatas thisBox;


    bool isActive;

    private void OnEnable()
    {
        gameManagerGO = GameObject.Find("Game Manager");

        _BM = gameManagerGO.GetComponent<BuildManager>();

        rend = gameObject.GetComponent<Renderer>();
        original_go_color = rend.material.color;

        if (GetComponent<CustomizeObjectLife>().enabled) GetComponent<CustomizeObjectLife>().enabled = false;

        GameObject.Find("Cancel_btn").GetComponent<Button>().interactable = true;

        thisBox = transform.parent.GetComponent<BoxDatas>();
    }

    public void DetermineState()
    {
        if (thisBox.box.blinkingFrq > 0)
        {
            if (GameManager.currentTurn == 0) isActive = true;

            if (thisBox.box.blinkingMode == 1)
            {
                if (GameManager.currentTurn % thisBox.box.blinkingFrq == 0)
                {
                    //Debug.Log("active " + thisBox.box.index);
                    ActiveObject(false);
                }
                else
                {
                    //Debug.Log("désactive" + thisBox.box.index);
                    if (isActive) DeactiveObject();
                }
            }
            else if (thisBox.box.blinkingMode == 0)
            {
                if (GameManager.currentTurn % thisBox.box.blinkingFrq == 0)
                {
                    //Debug.Log("désactive" + thisBox.box.index);
                    DeactiveObject();
                }
                else
                {
                    //Debug.Log("désactive" + thisBox.box.index);
                    if (!isActive) ActiveObject(true);
                }
            }
        }//end if (thisBox.box.blinkingFrq > 0)
    }

    public void ActiveObject(bool checkIfObjectIsActivated)
    {
        if (checkIfObjectIsActivated)
        {
            if (!isActive)
            {
                _BM.ActiveObject(transform.gameObject);
                isActive = true;
            }
        }
        else
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
}