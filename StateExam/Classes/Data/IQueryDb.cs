namespace DataLayer
{
    public interface IQueryDb<T, K>
    {
        bool Exists(K key);



    }
}