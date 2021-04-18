using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour
{
    [SerializeField] string sceneNameToLoad;
    [SerializeField] bool authorizeLevelEdition;

    public void f_LoadScene()
    {
        SceneManager.LoadScene(sceneNameToLoad);
        GeneralManager.isInBuildMode = authorizeLevelEdition;
    }
}
