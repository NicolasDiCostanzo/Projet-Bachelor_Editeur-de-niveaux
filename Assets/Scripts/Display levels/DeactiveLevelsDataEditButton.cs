using UnityEngine;

public class DeactiveLevelsDataEditButton : MonoBehaviour
{
    void Start()
    {
        if (!GeneralManager.isComingFromLocalLevelsChoice) gameObject.SetActive(false);

        enabled = false;
    }
}
