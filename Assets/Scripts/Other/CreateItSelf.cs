using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItSelf : MonoBehaviour
{
    GameObject instance;

    public void CreateThis(bool adaptParameters)
    {
        instance = Instantiate(this).gameObject;
        instance.name = gameObject.name;
        instance.transform.SetParent(GameObject.Find("Canvas").transform);

        if (adaptParameters) AdaptParameters();
    }

    void AdaptParameters()
    {
        if (instance)
        {
            RectTransform rectTransform = instance.GetComponent<RectTransform>();
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.localScale = Vector3.one;
        }
    }
}