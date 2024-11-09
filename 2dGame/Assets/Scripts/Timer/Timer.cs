using UnityEngine;

/// <summary>
/// Class for neatly tracking times
/// </summary>
public class Timer : MonoBehaviour
{
    public string Name;
    public float MaxTime { get; set; } = 1;
    public float CurrentTime { get; private set; } = 0;
    public bool Autostart { get; set; } = false;
    public bool Repeatable { get; set; } = true;

    private System.Action _onTimeout;

    /// <summary>
    /// Creates a timer that calls <c>onTimeout</c> when the timer hits 0.
    /// Timers marked not repeatable will destroy themselves after timing out,
    /// causing null errors if references of it are kept.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="name"></param>
    /// <param name="onTimeout"></param>
    /// <param name="maxTime"></param>
    /// <param name="autostart"></param>
    /// <param name="repeatable"></param>
    /// <returns>The timer created with the given values</returns>
    public static Timer CreateTimer(GameObject obj, string name, System.Action onTimeout, float maxTime, bool autostart = false, bool repeatable = true)
    {
        Timer timer = obj.AddComponent<Timer>();
        timer.Name = name;
        timer._onTimeout = onTimeout;
        timer.MaxTime = maxTime;
        timer.Autostart = autostart;
        timer.Repeatable = repeatable;

        timer.enabled = timer.Autostart;
        return timer;
    }

    private void Update()
    {
        Tick();
    }

    private void Tick()
    {
        CurrentTime += Time.deltaTime;
        if (CurrentTime >= MaxTime)
        {
            _onTimeout();

            if (Repeatable)
            {
                Reset();
            }
            else
            {
                Destroy(this);
            }
        }
    }

    /// <summary>
    /// Sets the current time to 0 and pauses/unpauses based on the value of autostart.
    /// </summary>
    public void Reset()
    {
        CurrentTime = 0;
        enabled = Autostart;
    }

    /// <summary>
    /// Starts the timer.
    /// </summary>
    public void StartTimer()
    {
        enabled = true;
    }

    /// <summary>
    /// Pauses the timer.
    /// </summary>
    public void StopTimer()
    {
        enabled = false;
    }
}