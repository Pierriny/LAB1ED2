using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class NodeAVL<T>
    {
        public T Value { get; set; }

        //Nodo Uno
        public NodeAVL<T>? One { get; set; }

        //Nodo Dos
        public NodeAVL<T>? Two { get; set; }

        public int Height { get; set; }

        //Constructor
        public NodeAVL(T Value)
        {
            this.Value = Value;
            One = null;
            Two = null;
            Height = 1;
        }
    }
}