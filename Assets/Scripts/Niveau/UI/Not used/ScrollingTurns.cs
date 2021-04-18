using UnityEngine;
using UnityEngine.UI;

public class ScrollingTurns : MonoBehaviour
{
    Slider slider;
    MovementManager _MM;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("scrolling turns !!!" + this.gameObject);
        _MM = GameObject.Find("Game Manager").GetComponent<MovementManager>();
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        slider.value = 0;
    }

    public void SeeTurn()
    {
        GameManager.currentTurn = (int)slider.value;

        for (int i = 0; i < FindObjectsOfType<ObjectBlinking>().Length; i++)
        {
            ObjectBlinking _object = FindObjectsOfType<ObjectBlinking>()[i];
            if (_object) _object.DetermineState();
        }

        //for (int i = 0; i < FindObjectsOfType<CustomizeObjectLife>().Length; i++)
        //{
        //    CustomizeObjectLife _object = FindObjectsOfType<CustomizeObjectLife>()[i];
        //    if (_object) _object.ObjectLifeManagement();
        //}
    }
}
