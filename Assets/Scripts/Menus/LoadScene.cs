using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour
{
    [SerializeField] string sceneNameToLoad;

    public void f_LoadScene() { SceneManager.LoadScene(sceneNameToLoad); }
}
