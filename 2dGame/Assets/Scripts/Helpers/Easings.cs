using UnityEngine;

/// <summary>
/// Class that holds various functions for easing animations or player movement
/// </summary>
public static class Easings
{
    public static float EaseOutSine(float t, float m)
    {
        return Mathf.Sin((t / m) * Mathf.PI / 2);
    }

    public static float EaseOutQuad(float t, float m)
    {
        return 1 - Mathf.Pow(1 - (t / m), 2);
    }

    public static float EaseInCubic(float t, float m)
    {
        return Mathf.Pow(t / m, 3);
    }

    public static float EaseOutCubic(float t, float m)
    {
        return 1 - Mathf.Pow(1 - (t / m), 3);
    }

    public static float EaseOutQuint(float t, float m)
    {
        return 1 - Mathf.Pow(1 - (t / m), 5);
    }

    public static float EaseOutExpo(float t, float m)
    {
        return (t == m) ? 1 : 1 - Mathf.Pow(2, -10 * t / m);
    }
}
