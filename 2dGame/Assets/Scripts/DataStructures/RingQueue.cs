using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class RingQueue<T>
{
    private T[] _queue;
    private int _length;
    public int Length { get => _length; }

    private int _head = 0;
    private int _tail = 0;
    public int Current { get => _head; }

    public RingQueue(int length = 8)
    {
        _length = length;
        _queue = new T[_length];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="toAdd"></param>
    public void Push(T toAdd)
    {
        if (EqualityComparer<T>.Default.Equals(_queue[_tail], default) || _tail != _head)
        {
            _queue[_tail] = toAdd;
            _tail = (_tail + 1) % _length;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public T Pop()
    {
        T toReturn = _queue[_head];
        _queue[_head] = default;
        _head = (_head + 1) % _length;
        return toReturn;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fromHead"></param>
    /// <returns></returns>
    public T Peek(int fromHead = 0)
    {
        return _queue[(_head + fromHead) % _length];
    }
}
