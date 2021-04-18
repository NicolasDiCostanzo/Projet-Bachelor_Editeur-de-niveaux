using UnityEngine;
using UnityEngine.UI;

public class SaveButtonManagement : MonoBehaviour
{
    private void OnDisable() { GetComponent<Button>().interactable = false; }
}
