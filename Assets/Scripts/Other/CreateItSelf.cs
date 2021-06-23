using UnityEngine;

public class CreateItSelf : MonoBehaviour
{
    GameObject instance;

    public void CreateThis(bool adaptParameters)
    {
        instance = Instantiate(this).gameObject;
        instance.name = gameObject.name;
        Debug.Log("create itself " + name + " " + GameObject.Find("Canvas"));
        instance.transform.SetParent(GameObject.Find("Canvas").transform, false);
        Debug.Log(instance.GetComponent<RectTransform>().offsetMin + " " + instance.GetComponent<RectTransform>().offsetMax);

        if (adaptParameters) AdaptParameters();
    }

    void AdaptParameters()
    {
        if (instance)
        {
            RectTransform rectTransform = instance.GetComponent<RectTransform>();
            Debug.Log(instance.GetComponent<RectTransform>());
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            Debug.Log(instance.transform.position);
        }
    }
}