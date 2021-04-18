using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HideButtonTextInEditor : MonoBehaviour
{
    void Start()
    {

#if UNITY_EDITOR
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            if (child.GetComponentInChildren<TextMeshProUGUI>()) child.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        } 
#endif

    }
}
