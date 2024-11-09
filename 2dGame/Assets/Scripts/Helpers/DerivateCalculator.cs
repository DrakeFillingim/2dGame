public static class DerivateCalculator
{
    private const float h = 0.01f;

    /// <summary>
    /// Faster method used to derive a function f that has an input value of t
    /// and constant value m to control the function
    /// </summary>
    /// <param name="f"></param>
    /// <param name="t"></param>
    /// <param name="m"></param>
    /// <returns></returns>
    public static float ThreePointDerive(System.Func<float, float, float> f, float t, float m = 1)
    {
        return (f(t + h, m) - f(t - h, m)) / (2 * h);
    }

    /// <summary>
    /// Slower method used to derive a function f that has an input value of t
    /// and constant value m to control the function
    /// </summary>
    /// <param name="f"></param>
    /// <param name="t"></param>
    /// <param name="m"></param>
    /// <returns></returns>
    public static float FivePointDerive(System.Func<float, float, float> f, float t, float m = 1)
    {
        return (f(t - (2 * h), m) - (8 * f(t - h, m)) + (8 * f(t + h, m)) - f(t + (2 * h), m)) / (12 * h);
    }
}
