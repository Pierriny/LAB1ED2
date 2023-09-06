using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class AVLTree<T> : IEnumerable<T>
    {
        public NodeAVL<T>? Root;
        public AVLTree()
        {
            Root = null;
        }
        public bool IsEmpty() // Función que indica si el árbol se encuentra vacío
        {
            return this.Root == null;
        }
        public int GetBalance(NodeAVL<T> Node_GB) // Se encarga de encontrar el factor de balanceo del nodo que se le envia 
        {
            if (Node_GB.One == null && Node_GB.Two == null)// Verifación donde el factor de balanceo es 0 (no tiene hijos)
            {
                return 0;
            }
            else if (Node_GB.One == null)// Verifación donde el factor de balanceo es -1 (hijo izquierda)
            {
                return -Node_GB.Two!.Height;

            }
            else if (Node_GB.Two == null)// Verifación donde el factor de balanceo es 1 (hijo derecha)
            {
                return Node_GB.One!.Height;
            }
            else
            {
                return Node_GB.One!.Height - Node_GB.Two!.Height;
            }
        }

        private void SetHeight(NodeAVL<T> Nodo_H) // Obtiene la altura que posee el nodo enviado
        {
            //Se toma el nodo hijo con mayor altura
            if (Nodo_H.One == null || Nodo_H.Two == null)
            {
                if (Nodo_H.One == null && Nodo_H.Two == null)
                {
                    Nodo_H.Height = 1;
                }
                else if (Nodo_H.One == null)
                {
                    Nodo_H.Height = 1 + Nodo_H.Two!.Height;
                }
                else
                {
                    Nodo_H.Height = 1 + Nodo_H.One.Height;
                }
            }
            else if (Nodo_H.One!.Height > Nodo_H.Two!.Height) // Verifica si el nodo "One" es mayor
            {
                Nodo_H.Height = 1 + Nodo_H.One!.Height; //El nodo "One" es mayor
            }
            else
            {
                Nodo_H.Height = 1 + Nodo_H.Two!.Height; //El nodo "One" es menor
            }
        }

        public void Add(T item, Delegate CompareObj)
        {
            Root = AddInAVL(Root!, item, CompareObj);
        }
        private NodeAVL<T> AddInAVL(NodeAVL<T> Nodo, T item, Delegate CompareObj) //Proceso para añadir un nuevo elemento a la estructura AVL
        {
            if (Nodo == null)
            {
                Nodo = new NodeAVL<T>(item);
            }
            else
            {// Verificación para saber si el nuevo elemento debe de ir a la izquierda o a la derecha de la posicion actual
                if ((int)CompareObj.DynamicInvoke(item, Nodo.Value) < 0) //El elemento ingresado es menor al valor del nodo actual
                {
                    Nodo.One = AddInAVL(Nodo.One!, item, CompareObj);
                }
                else if ((int)CompareObj.DynamicInvoke(item, Nodo.Value) > 0)//El elemento ingresado es mayor al valor del nodo actual
                {
                    Nodo.Two = AddInAVL(Nodo.Two!, item, CompareObj);
                }
            }
            Nodo = ReBalance(Nodo);
            return Nodo;
        }

        public void Delete(T item, Delegate CompareObj)
        {
            Root = DeleteInAVL(Root!, item, CompareObj);
        }

        public void RemoveAll()
        {
            Root = null;
        }

        private NodeAVL<T>? DeleteInAVL(NodeAVL<T>? Nodo, T item, Delegate CompareObj) //¨Poner el nodo anterior???
        {
            if (Nodo == null)
            {
                return Nodo;
            }
            else if ((int)CompareObj.DynamicInvoke(item, Nodo.Value) < 0) //El valor a eliminar es menor al valor del nodo
            {
                Nodo.One = DeleteInAVL(Nodo.One!, item, CompareObj);
            }
            else if ((int)CompareObj.DynamicInvoke(item, Nodo.Value) > 0) //El valor a eliminar es mayor al valor del nodo actual
            {
                Nodo.Two = DeleteInAVL(Nodo.Two!, item, CompareObj);
            }
            else
            {
                if (Nodo.One == null && Nodo.Two == null) //Si cumple el nodo es una hoja
                {
                    Nodo = null;
                    return Nodo;
                }
                else if (Nodo.One == null && Nodo.Two != null)
                {
                    NodeAVL<T> temp = Nodo.Two;
                    Nodo.Two = null;
                    Nodo = temp;
                }
                else if (Nodo.One != null && Nodo.Two == null)
                {
                    NodeAVL<T> temp = Nodo.One;
                    Nodo.One = null;
                    Nodo = temp;
                }
                else
                {
                    NodeAVL<T>? temp = RightestfromLeft(Nodo.One!);
                    Nodo.Value = temp!.Value;
                    Nodo.One = DeleteInAVL(Nodo.One!, temp.Value, CompareObj);
                }
            }
            Nodo = ReBalance(Nodo);
            return Nodo;
        }
        private NodeAVL<T> RightRotation(NodeAVL<T> nodo)// Rotación a la derecha
        {
            NodeAVL<T> temp = nodo.One!;
            nodo.One = temp.Two!;
            temp.Two = nodo;
            SetHeight(nodo);
            SetHeight(temp);
            return temp;
        }

        private NodeAVL<T> LeftRotation(NodeAVL<T> nodo)//Rotación a la izquirda
        {
            NodeAVL<T> temp = nodo.Two!;
            nodo.Two = temp.One!;
            temp.One = nodo;
            SetHeight(nodo);
            SetHeight(temp);
            return temp;
        }

        private NodeAVL<T> DoubleRightRotation(NodeAVL<T> nodo)//Rotación doble a la derecha
        {
            NodeAVL<T> temp = nodo.One!;
            temp = LeftRotation(temp);
            nodo.One = temp;
            nodo = RightRotation(nodo);
            return nodo;
        }

        private NodeAVL<T> DoubleLeftRotation(NodeAVL<T> nodo)//Rotación doble a la izquierda
        {
            NodeAVL<T> temp = nodo.Two!;
            temp = RightRotation(temp);
            nodo.Two = temp;
            nodo = LeftRotation(nodo);
            return nodo;
        }

        private NodeAVL<T>? RightestfromLeft(NodeAVL<T>? nodo)
        {
            NodeAVL<T> nodoTemp = nodo;
            while (nodoTemp!.Two != null)
            {
                nodoTemp = nodoTemp.Two;
            }
            return nodoTemp;
        }

        private NodeAVL<T> ReBalance(NodeAVL<T> nodo)
        {
            SetHeight(nodo); //Se actualiza la altura del nodo
            int FE = GetBalance(nodo); //Se obtiene el factor de equilibrio del nodo
            if (FE < -1)
            {
                if (GetBalance(nodo.Two!) < 0)
                {
                    nodo = LeftRotation(nodo);
                }
                else
                {
                    nodo = DoubleLeftRotation(nodo);
                }
            }
            else if (FE > 1)
            {
                if (GetBalance(nodo.One!) > 0)
                {
                    nodo = RightRotation(nodo);
                }
                else
                {
                    nodo = DoubleRightRotation(nodo);
                }
            }
            return nodo;
        }

        public IEnumerable<T> FindAll(T Data, NodeAVL<T>? Node, Delegate Comparison)
        {
            if (Node != null)
            {
                if (Node.One != null)
                {
                    FindAll(Data, Node.One, Comparison);
                }
                if ((int)Comparison.DynamicInvoke(Data, Node.Value) == 0)
                {
                    yield return Node.Value;
                }
                if (Node.Two != null)
                {
                    FindAll(Data, Node.Two, Comparison);
                }
            }
        }

        public void InOrderRoute(NodeAVL<T>? Node, Queue<T> Items)
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

        public NodeAVL<T> SearchElement(T ElementToSearch, Delegate Compare)
        {
            NodeAVL<T>? Temp = Root;
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
        }


    }
}