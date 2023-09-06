using System;
namespace LAB2_ED2.Models
{
    public class Delegates
    {
        public static System.Comparison<int> CompareInt = delegate (int M1, int M2)
        {
            return M1.CompareTo(M2);
        };

        public static System.Comparison<string> CompareString = delegate (string M1, string M2)
        {
            return M1.CompareTo(M2);
        };

        public static System.Comparison<char> CompareChar = delegate (char M1, char M2)
        {
            return M1.CompareTo(M2);
        };

        public static System.Comparison<System.DateTime> CompareDateTime = delegate (System.DateTime M1, System.DateTime M2)
        {
            return M1.CompareTo(M2);
        };

        public static Comparison<PeopleModel> SortingNames = delegate (PeopleModel m1, PeopleModel m2)
        {
            return m1.name.CompareTo(m2.name);
        };

        public static System.Comparison<PeopleModel> DPIComparison = delegate (PeopleModel M1, PeopleModel M2)
        {
            return M1.dpi.CompareTo(M2.dpi);
        };

        public static Comparison<CodeModel> SymbolCompare = delegate (CodeModel m1, CodeModel m2)
        {
            return m1.Simbolo.CompareTo(m2.Simbolo);
        };

    }
}
