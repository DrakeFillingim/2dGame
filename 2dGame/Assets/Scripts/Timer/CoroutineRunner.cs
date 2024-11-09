using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private IEnumerator _coroutine;

    public static CoroutineRunner CreateCoroutine(GameObject obj, IEnumerator coroutine)
    {
        CoroutineRunner runner = obj.AddComponent<CoroutineRunner>();
        runner._coroutine = coroutine;
        return runner;
    }

    public void Update()
    {
        if(!_coroutine.MoveNext())
        {
            Destroy(this);
        }
    }
}
