using UnityEngine;

public class ManageTutoArrow : MonoBehaviour
{
    void Start()
    {
        if (SaveSystem.GetBinarySavedData().levelReached > 0 || SaveSystem.GetBinarySavedData().editionUnlocked) gameObject.SetActive(false);
    }
}
