namespace GraphQL.Queries.Secondary.Types
{
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.EntityFrameworkCore;

    public struct GroupPath : IEquatable<GroupPath>
    {
        [ActivatorUtilitiesConstructor]
        public GroupPath() => _value = string.Empty;

        public GroupPath(string path) : this()
        {
            _value = path;

            NLevel = path.Split('.').Length-1;
        }

        private readonly string _value;

        public int NLevel { get; set;  }

        public bool IsAncestorOf(string s)
        {
            throw new InvalidOperationException(CoreStrings.FunctionOnClient("IsAncestorOf"));
        }

        //
        // Summary:
        //     Returns whether this ltree is a descendant of other (or equal).
        //
        // Remarks:
        //     The method call is translated to left <@ right. See https://www.postgresql.org/docs/current/ltree.html
        public bool IsDescendantOf(string a)
        {
            throw new InvalidOperationException(CoreStrings.FunctionOnClient("IsDescendantOf"));
        }

        //
        // Summary:
        //     Converts an Microsoft.EntityFrameworkCore.LTree type to a string.
        public static implicit operator GroupPath(string value)
        {
            return new GroupPath(value);
        }

        //
        // Summary:
        //     Converts a string to an Microsoft.EntityFrameworkCore.LTree type.
        public static implicit operator string(GroupPath ltree)
        {
            return ltree._value;
        }

        //
        // Summary:
        //     Compares two Microsoft.EntityFrameworkCore.LTree instances for equality.
        public static bool operator ==(GroupPath x, GroupPath y)
        {
            return x._value == y._value;
        }

        //
        // Summary:
        //     Compares two Microsoft.EntityFrameworkCore.LTree instances for inequality.
        public static bool operator !=(GroupPath x, GroupPath y)
        {
            return x._value != y._value;
        }

        public bool Equals(GroupPath other)
        {
            return _value == other._value;
        }

        public override bool Equals(object? obj)
        {
            if (obj is LTree other)
            {
                return Equals(other);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (_value == null)
            {
                return 0;
            }

            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return _value;
        }
    }
}
