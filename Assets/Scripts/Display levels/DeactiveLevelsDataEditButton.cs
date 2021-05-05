using UnityEngine;

public class DeactiveLevelsDataEditButton : MonoBehaviour
{
    void Start()
    {
        if (!GeneralManager.isComingFromLocalLevelsChoice) gameObject.SetActive(false);

        if (!transform.parent.parent.GetComponent<WindowConstructor>().level.completed) GetComponent<UnityEngine.UI.Button>().interactable = false;

        enabled = false;
    }
}
