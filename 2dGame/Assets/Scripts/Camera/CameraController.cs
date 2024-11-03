using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const int MaxFramerate = 144;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = MaxFramerate;
    }
}