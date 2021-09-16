namespace NwaySetAssociativeCache
{
    public interface IReplacementAlgorithm<K>
    {
        void OnGet(K key);
        void OnSetNew(K key);
        void OnSetUpdate(K key);
        K GetKeyToRemove();
        void OnRemove();
    }
}
