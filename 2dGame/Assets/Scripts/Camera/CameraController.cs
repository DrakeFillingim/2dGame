using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const int MaxFramerate = 60;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = MaxFramerate;
    }
}