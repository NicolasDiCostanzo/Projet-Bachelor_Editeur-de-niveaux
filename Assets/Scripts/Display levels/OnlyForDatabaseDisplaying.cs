using UnityEngine;

public class OnlyForDatabaseDisplaying : MonoBehaviour
{
    void Start()
    {
        if (!GeneralManager.isComingFromDatabaseLevelsChoice) Destroy(gameObject);
    }
}
