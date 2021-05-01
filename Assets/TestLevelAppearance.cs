using UnityEngine;

public class TestLevelAppearance : MonoBehaviour
{
    void Start()
    {
        if (!GeneralManager.isInBuildMode) Destroy(gameObject);
    }
}
