using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class BSTree<T> : IEnumerable<T>
    {
        public Node<T>? Root;
        bool Empty;
        public BSTree()
        {
            Root = null;
            Empty = true;
        }
        public void InsertBinaryTree(T Data, Delegate CompareObj)
        {
            Node<T> NewNode = new Node<T>(Data);
            NewNode.Value = Data;
            NewNode.One = null; //Left
            NewNode.Two = null; //Right
            if (Root == null)
            {
                Root = NewNode;
                Empty = false;
            }
            else
            {
                Node<T>? Temp, PreTemp;
                Temp = Root;
                PreTemp = null;
                while (Temp != null)
                {
                    PreTemp = Temp;
                    if ((int)CompareObj.DynamicInvoke(Data, Temp.Value)>0)
                    {
                        Temp = Temp.Two;
                    }
                    else
                    {
                        Temp = Temp.One;
                    }
                }
                if ((int)CompareObj.DynamicInvoke(Data, PreTemp!.Value)>0)
                {
                    PreTemp.Two = NewNode;
                }
                else
                {
                    PreTemp.One = NewNode;
                }
            }
        }

        public void Remove(Node<T> NodeRootToSearch, Node<T> NodeSearch, Delegate CompareObj)
        {
            Node<T>? current = NodeRootToSearch;
            Node<T>? parent = NodeRootToSearch;
            bool IsChildLeft = false;


            if (current == null)
            {
                return;
            }

            while (current != null && (int)CompareObj.DynamicInvoke(current.Value, NodeSearch.Value) != 0)
            {
                parent = current;

                if ((int)CompareObj.DynamicInvoke(NodeSearch.Value, current.Value) /*<*/ < 0)
                {
                    current = current.One;
                    IsChildLeft = true;
                }
                else
                {
                    current = current.Two;
                    IsChildLeft = false;
                }
            }

            if (current == null)
            {
                return;
            }

            if (current.Two == null && current.One == null)
            {
                if (current == Root)
                {
                    Root = null;
                }
                else
                {
                    if (IsChildLeft)
                    {
                        parent.One = null;
                    }
                    else
                    {
                        parent.Two = null;
                    }
                }
            }
            else if (current.Two == null)
            {
                if (current == Root)
                {
                    Root = current.One;
                }
                else
                {
                    if (IsChildLeft)
                    {
                        parent.One = current.One;
                    }
                    else
                    {
                        parent.Two = current.One;
                    }
                }
            }
            else if (current.One == null)
            {
                if (current == Root)
                {
                    Root = current.Two;
                }
                else
                {
                    if (IsChildLeft)
                    {
                        parent.One = current.Two;
                    }
                    else
                    {
                        parent.Two = current.Two;
                    }
                }
            }
            else
            {
                if (current == Root)
                {
                    Node<T> successor = RightestfromLeft(current);
                    Root = successor;
                }
                else if (IsChildLeft)
                {
                    Node<T> successor = MostLeftFromRight(current);
                    parent.One = successor;
                }
                else
                {
                    Node<T> successor = RightestfromLeft(current);
                    parent.Two = successor;
                }
            }
        }

        private Node<T> RightestfromLeft (Node<T> Node)
        {
            Node<T>? Temp = Node.One;
            Node<T>? PreTemp = Node.One;
            Node<T>? CurrentLeft = Node.One;
            Node<T>? CurrentRight = Node.Two;
            bool NoSubTree = true;

            while (Temp!.Two != null) 
            {
                PreTemp = Temp;
                Temp = Temp.Two;
                NoSubTree = false;
            }
            PreTemp.Two = null;
            if (NoSubTree)
            {
                Temp.One = CurrentLeft!.One;
            }
            else
            {
                Temp.One = CurrentLeft;
            }
            Temp.Two = CurrentRight;
            return Temp; 
        }

        private Node<T> MostLeftFromRight (Node<T> Node)
        {
            Node<T>? Temp = Node.Two;
            Node<T>? PreTemp = Node.Two;
            Node<T>? CurrentLeft = Node.One;
            Node<T>? CurrentRight = Node.Two;
            bool NoSubTree = true;

            while (Temp!.One != null)
            {
                PreTemp = Temp;
                Temp = Temp.One;
                NoSubTree = false;
            }
            PreTemp.One = null;
            Temp.One = CurrentLeft;
            if (NoSubTree)
            {
                Temp.Two = CurrentRight!.Two;
            }
            else 
            {
                Temp.Two = CurrentRight;
            }
            return Temp;
        }

        public void InOrderRoute(Node<T>? Node, Queue<T> Items)
        {
            if (Node != null)
            { 
                if (Node!.One != null)
                {
                    InOrderRoute(Node.One, Items);
                }
                Items.Enqueue(Node.Value);
                if (Node!.Two != null)
                {
                    InOrderRoute(Node.Two, Items);
                }
            }
        }

        public Node<T> SearchElement(T ElementToSearch, Delegate Compare)
        {
            Node<T>? Temp = Root;
            while (Temp != null && (int)Compare.DynamicInvoke(ElementToSearch, Temp!.Value) != 0)
            {
                if ((int)Compare.DynamicInvoke(ElementToSearch, Temp!.Value) > 0)
                {
                    Temp = Temp.Two;
                }
                else 
                {
                    Temp = Temp.One;
                }
            }
            return Temp;
            //if (Temp != null)
            //{
            //    return Temp;
            //}
            //else 
            //{
            //    throw new InvalidOperationException("El objeto no se ha encontrado");
            //}  
        }

        public IEnumerator<T> GetEnumerator()
        {
            Queue<T> Elements = new Queue<T>();
            InOrderRoute(Root, Elements);
            while (Elements.Count != 0)
            {
                yield return Elements.Dequeue();
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
