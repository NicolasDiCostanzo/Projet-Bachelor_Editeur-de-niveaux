using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageContinuButtonInteractability : MonoBehaviour
{
    void Start()
    {
        Debug.Log(SaveSystem.GetBinarySavedData().levelReached + " " + GeneralManager._levelToReachToUnlockLevelCreation);
        if (SaveSystem.GetBinarySavedData().levelReached > 0) GetComponent<UnityEngine.UI.Button>().interactable = true;
    }
}
