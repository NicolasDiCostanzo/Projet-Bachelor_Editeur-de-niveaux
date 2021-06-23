using System.Collections;
using UnityEngine;

public class ManageLevelEditorButtonInteractability : MonoBehaviour
{
    IEnumerator DetermineInteractability()
    {
        yield return new WaitForEndOfFrame();

        if (SaveSystem.GetBinarySavedData().editionUnlocked) GetComponent<UnityEngine.UI.Button>().interactable = true;
        GetComponentInChildren<ChangeTextColorOnMouseOver>().DetermineColorOnStart();

    }
    void OnEnable()
    {
        StartCoroutine(DetermineInteractability());
    }
}
