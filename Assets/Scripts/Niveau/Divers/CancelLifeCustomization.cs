using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ce script permet de supprimer les comportements attachés à cet objet comme le 'Object Blinking' et le 'Customize Object Life'
/// </summary>
public class CancelLifeCustomization : MonoBehaviour
{
    public void _CancelLifeCustomization()
    {
        GameObject linkedGameObject = transform.parent.GetComponent<LinkedGameObject>().linkedGameObject;

        ObjectBlinking objectBlinking_script = null;

        if (linkedGameObject.GetComponent<ObjectBlinking>()) objectBlinking_script = linkedGameObject.GetComponent<ObjectBlinking>();
        CustomizeObjectLife customizeLifeObject_script = linkedGameObject.GetComponent<CustomizeObjectLife>();

        LevelBoardBox thisBox = linkedGameObject.transform.parent.GetComponent<BoxDatas>().box;


        if (objectBlinking_script.enabled && objectBlinking_script != null)
        {
            thisBox.blinkingFrq = 0;
            thisBox.blinkingMode = 0;
            linkedGameObject.GetComponent<Renderer>().material.color = objectBlinking_script.original_go_color;

            objectBlinking_script.enabled = false;
        }

        if (customizeLifeObject_script.enabled)
        {
            thisBox.buildTurn = 0;
            thisBox.destroyTurn = 0;
            linkedGameObject.GetComponent<Renderer>().material.color = customizeLifeObject_script.originial_go_color;

            customizeLifeObject_script.enabled = false;
        }

        GetComponent<Button>().interactable = false;
    }
}
