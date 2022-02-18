[System.Serializable]
public struct NullableVar<T>
{
    public T Value;
    public bool IsNull;

    public NullableVar(T value, bool isNull = false)
    {
        Value = value;
        IsNull = isNull;
    }
}
