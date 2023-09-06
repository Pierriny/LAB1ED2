namespace DataStructures
{
    internal interface IDataStructure<T>
    {
        void Insert(T Value);
        void Remove(int pos);
        void Update(T Value, int pos);
        T GetValue(int pos);
        Node<T> GetNode(int pos);

        void Clear();
    }
}
