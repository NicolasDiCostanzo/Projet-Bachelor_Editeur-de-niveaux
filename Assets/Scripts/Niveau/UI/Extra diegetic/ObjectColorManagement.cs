using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColorManagement : MonoBehaviour
{
    Material objectMaterial;
    Color originalColor;
    LinkedGameObject linkedGameObject_script;

    [SerializeField] Color colorWhenHighlighted;

    void Start()
    {
        objectMaterial = GetComponent<Renderer>().material;
        originalColor = objectMaterial.color;
        linkedGameObject_script = GameObject.Find("CustomisationPanel").GetComponent<LinkedGameObject>();
    }

    void Update()
    {
        if (linkedGameObject_script.linkedGameObject == gameObject)                                     objectMaterial.color = colorWhenHighlighted;
        else if (GetComponent<ObjectBlinking>().enabled || GetComponent<CustomizeObjectLife>().enabled) objectMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, .5f);
        else                                                                                            objectMaterial.color = originalColor;

    }
}
