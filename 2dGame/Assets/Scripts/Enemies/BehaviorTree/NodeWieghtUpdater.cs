using UnityEngine;

public class NodeWieghtUpdater : MonoBehaviour
{
    private static NodeWieghtUpdater _instance;
    private event System.Action UpdateEvent;
    
    public static void AddNodeWieght(System.Action nodeUpdate)
    {
        if (_instance == null)
        {
            _instance = new GameObject("WeightUpdater").AddComponent<NodeWieghtUpdater>();
        }
        _instance.UpdateEvent += nodeUpdate;
    }

    private void Update()
    {
        UpdateEvent?.Invoke();
    }
}