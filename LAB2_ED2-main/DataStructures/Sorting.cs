using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class Sorting<T>
    {
        public void BubbleSortAsc<T>(int Size, int Condition, DataStructures.DoubleLinkedList<T> List, Delegate CompareObj) //Ordenamiento ascendente por nombre
        {
            int i, j;
            for (i = 0; i < Size - 1; i++)
            {
                for (j = 0; j < Size - 1 - i; j++)
                {
                    if ((int)CompareObj.DynamicInvoke(List.GetNode(j), List.GetNode(j+1), Condition) == 1) //Compara si es mayor
                    {
                        var Object1 = List.GetNode(j);
                        var Object2 = List.GetNode(j+1);
                        var AUX = Object1.Value; //Variable auxiliar
                        Object1.Value = Object2.Value;
                        Object2.Value = AUX;
                    }
                }
            }
        }

        public void BubbleSortDes<T>(int Size, int Condition, DataStructures.DoubleLinkedList<T> List, Delegate CompareObj) //Ordenamiento ascendente por nombre
        {
            int i, j;
            for (i = 0; i < Size - 1; i++)
            {
                for (j = 0; j < Size - 1 - i; j++)
                {
                    if ((int)CompareObj.DynamicInvoke(List.GetNode(j), List.GetNode(j + 1), Condition) == -1) //Compara si es mayor
                    {
                        var Object1 = List.GetNode(j);
                        var Object2 = List.GetNode(j + 1);
                        var AUX = Object1.Value; //Variable auxiliar
                        Object1.Value = Object2.Value;
                        Object2.Value = AUX;
                    }
                }
            }
        }

    }
}
