using UnityEngine;
using UnityEngine.UI;

public class SaveButtonManagement : MonoBehaviour
{
    void OnDisable() { GetComponent<Button>().interactable = false; }
}
