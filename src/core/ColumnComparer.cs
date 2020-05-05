using System.Collections.Generic;

namespace System.Linq
{
    internal class ColumnComparer : IEqualityComparer<object[]>
    {
        public bool Equals(object[] x, object[] y) => x.SequenceEqual(y);

        public int GetHashCode(object[] obj) => string.Join("", obj.ToArray()).GetHashCode();
    }
}