using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int MaxFramerate = 144;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = MaxFramerate;
    }
}