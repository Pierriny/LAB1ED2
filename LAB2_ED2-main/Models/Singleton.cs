using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB2_ED2.Models
{
    public sealed class Singleton
    {
        public readonly DataStructures.AVLTree<PeopleModel> AVLTree;
        public readonly DataStructures.DoubleLinkedList<CodeModel> CodArray;
        
        public int RotationCount;
        public int CompararisonCount;
        public System.Collections.Generic.List<PeopleModel>? SearchedItems;
        public LAB2_ED2.Models.PeopleModel? SearchedItem;

        private Singleton()
        {
            AVLTree = new DataStructures.AVLTree<PeopleModel>();
            CodArray = new DataStructures.DoubleLinkedList<CodeModel>();
          
            RotationCount = 0;
            CompararisonCount = 0;
        }
        private static readonly object PadLock = new object();
        private static Singleton instance = null;
        public static Singleton Instance { get { if (instance == null) { lock (PadLock) { if (instance == null) { instance = new Singleton(); } } } return instance; } }
    }
}
