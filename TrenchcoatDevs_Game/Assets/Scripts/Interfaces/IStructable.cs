public interface IStructable<T> where T : struct
{
    public T StructForm();
    public void SetFromStruct(T netStruct);
}