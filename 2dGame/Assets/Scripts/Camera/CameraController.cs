using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int MaxFramerate = 144;
    public bool followPlayer = true;
    private Transform _player;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = MaxFramerate;
        _player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (!followPlayer)
        {
            return;
        }
        transform.position = new Vector3(_player.position.x, _player.position.y, transform.position.z);
    }
}