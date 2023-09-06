namespace DataStructures
{
    public class Node<T>
    {
        public T Value { get; set; }

        //Nodo Uno
        public Node<T>? One { get; set; }

        //Nodo Dos
        public Node<T>? Two { get; set; }

        //Constructor
        public Node(T Value)
        {
            this.Value = Value;
            One = null;
            Two = null;
        }
    }
}
