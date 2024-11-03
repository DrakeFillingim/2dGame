/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class OverwritableStack<T>
{
    private T[] _stack;
    private int _length;
    public int Length { get => _length; }

    private int _index = -1;
    public int Current { get => _index; }

    public OverwritableStack(int length = 8)
    {
        _length = length;
        _stack = new T[_length];
    }

    /// <summary>
    /// Add a state to the stack, overwriting the oldest if the number of states exceeds the max.
    /// </summary>
    /// <param name="toAdd"></param>
    public void Push(T toAdd)
    {
        _index = (_index + 1) % _length;
        _stack[_index] = toAdd;
    }

    /// <summary>
    /// Removes and returns the most recent state added to the stack, or default of the stack is empty.
    /// </summary>
    /// <returns></returns>
    public T Pop()
    {
        T toReturn = _stack[_index];
        _stack[_index] = default;
        _index = (_length + _index - 1) % _length;
        return toReturn;
    }
}
