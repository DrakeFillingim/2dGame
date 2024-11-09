using UnityEngine;

public class Timer : MonoBehaviour
{
    public string Name;
    public float MaxTime { get; set; } = 1;
    public float CurrentTime { get; private set; } = 0;
    public bool Autostart { get; set; } = false;
    public bool Repeatable { get; set; } = true;

    private System.Action _onTimeout;

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

    public void Reset()
    {
        CurrentTime = 0;
        enabled = Autostart;
    }

    public void StartTimer()
    {
        enabled = true;
    }

    public void StopTimer()
    {
        enabled = false;
    }
}