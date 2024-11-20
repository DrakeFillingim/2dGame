using System.Collections;
using UnityEngine;

/// <summary>
/// Class for running coroutine style functions without a monobehavior
/// </summary>
public class CoroutineRunner : MonoBehaviour
{
    private IEnumerator _coroutine;

    /// <summary>
    /// Creates a new runner that "calls" the given function once per fixed update frame.
    /// Pass in the <c>IEnumerator</c> returned by calling the coroutine function.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="coroutine"></param>
    /// <returns>The coroutine runner, typically ignored</returns>
    public static CoroutineRunner CreateCoroutine(GameObject obj, IEnumerator coroutine)
    {
        CoroutineRunner runner = obj.AddComponent<CoroutineRunner>();
        runner._coroutine = coroutine;
        return runner;
    }

    private void FixedUpdate()
    {
        if(!_coroutine.MoveNext())
        {
            Destroy(this);
        }
    }

    public void StopCoroutine()
    {
        Destroy(this);
    }
}
