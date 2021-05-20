using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomisationPanelDefaultValues : MonoBehaviour
{
    [SerializeField] InputField buildAt_if, destroyAt_if;
    // Start is called before the first frame update

    private void OnEnable()
    {
        LevelBoardBox linkedBox = GetComponentInParent<LinkedGameObject>().linkedGameObject.transform.parent.GetComponent<BoxDatas>().box;

        if (linkedBox != null)
        {
            if (linkedBox.buildTurn != 0) buildAt_if.text = linkedBox.buildTurn.ToString();
            if (linkedBox.destroyTurn != 0) destroyAt_if.text = linkedBox.destroyTurn.ToString();
        }

    }

    private void OnDisable()
    {
        buildAt_if.text = "";
        destroyAt_if.text = "";
    }
}
