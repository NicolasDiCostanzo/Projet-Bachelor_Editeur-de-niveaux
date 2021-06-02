using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItSelf : MonoBehaviour
{
    [SerializeField] RectTransform parent;
    
    public void CreateThis()
    {
        Instantiate(this);
        name = gameObject.name;
        transform.parent = GameObject.Find("Canvas").transform;
    }
}
