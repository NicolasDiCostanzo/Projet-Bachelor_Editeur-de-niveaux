using Cinemachine;
using UnityEngine;

public class ChangeCameraView : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera orthographicCamera, perspectiveCamera;

    public void ChangeCameraPerspective()
    {
        int priorityMemorizer = orthographicCamera.m_Priority;

        orthographicCamera.m_Priority = perspectiveCamera.m_Priority;
        perspectiveCamera.m_Priority = priorityMemorizer;
    }
}
