using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    public class DoubleLinkedList<T> : IDataStructure<T>, IEnumerable<T>
    {
        public int Count { get; set; }
        bool Empty { get; set; }
        public Node<T>? Head { get; set; }
        public Node<T>? Tail { get; set; }
        public T GetValue(int pos)
        {
            Node<T> iterador = Head!;
            for (int i = 0; i < pos; i++)
            {
                iterador = iterador.Two!;
            }
            return iterador.Value;
        }

        public void Insert(T Value)
        {
            var New = new Node<T>(Value);
            if (Count == 0)
            {
                Head = New;
                Tail = New;
                Empty = false;
            }
            else
            {
                Tail!.Two = New;
                New.One = Tail;
                Tail = New;
            }
            Count++;
        }

        public void Remove(int pos)
        {
            if (pos == 0 && Count != 1)
            {
                Node<T> auxiliar = Head!;
                Head = Head!.Two;
                Head!.One = null;
                auxiliar.Two = null;
            }
            else if (Count == 1)
            {
                Head = Head!.Two;
                Tail = Tail!.One;
                Empty = true;
            }
            else if (pos == Count - 1)
            {
                Node<T> Aux = Tail!;
                Tail = Tail!.One;
                Tail!.Two = null;
                Aux.One = null;
            }
            else
            {
                Node<T> iterador = Head!;
                for (int i = 0; i < pos; i++)
                {
                    iterador = iterador.Two!;
                }
                iterador!.One!.Two = iterador.Two;
                iterador.Two!.One = iterador.One;
            }
            Count--;
        }

        public void Update(T Value, int pos)
        {
            Node<T> iterador = Head!;
            for (int i = 0; i < pos; i++)
            {
                iterador = iterador.Two!;
            }
            iterador.Value = Value;
        }

        public Node<T> GetNode(int pos)
        {
            Node<T> iterador = Head!;
            for (int i = 0; i < pos; i++)
            {
                iterador = iterador.Two!;
            }
            return iterador;
        }

        public void Clear()
        {
            while (Count > 0)
            {
                Remove(0);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var Node = Head;
            while (Node != null) 
            { 
                yield return Node.Value;
                Node = Node.Two;
            }
        }

        private IEnumerator GetEnumerator1() 
        {
            return this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() 
        {
            return GetEnumerator1();
        }
    }
}
