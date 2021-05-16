using UnityEngine;

public class DisplayFilter : MonoBehaviour
{
    void Start()
    {
        if (!GeneralManager.isComingFromDatabaseLevelsChoice) Destroy(gameObject);
    }
}
