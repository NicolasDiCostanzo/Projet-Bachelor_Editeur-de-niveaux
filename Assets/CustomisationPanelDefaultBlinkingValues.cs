using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomisationPanelDefaultBlinkingValues : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] Dropdown dropdown;

    private void OnEnable()
    {
        LevelBoardBox linkedBox = GetComponentInParent<LinkedGameObject>().linkedGameObject.transform.parent.GetComponent<BoxDatas>().box;

        if (linkedBox != null)
        {
            if (linkedBox.blinkingFrq != 0) inputField.text = linkedBox.blinkingFrq.ToString();
            dropdown.value = linkedBox.blinkingMode;
        }
    }

    private void OnDisable()
    {
        inputField.text = "";
        dropdown.value = 0;
    }
}
