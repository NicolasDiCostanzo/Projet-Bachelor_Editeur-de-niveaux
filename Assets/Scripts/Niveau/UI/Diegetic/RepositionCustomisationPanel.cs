using UnityEngine;

public class RepositionCustomisationPanel : MonoBehaviour
{
    Vector2 originalPos;

    void Awake()
    {
        originalPos = transform.position;
    }

    public void RepositionPanel()
    {
        transform.position = originalPos;
        GetComponent<LinkedGameObject>().linkedGameObject = null;
    }

}
