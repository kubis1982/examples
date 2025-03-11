using HotChocolate.Language;

namespace GraphQL.Queries.Secondary.Types;

public class GroupPathType : ScalarType<GroupPath, StringValueNode>
{
    public GroupPathType() : base(nameof(GroupPathType))
    {
        Description = "Represents a hierarchical path in a group structure";
    }

    public override IValueNode ParseResult(object? resultValue)
    {
        return new StringValueNode((string)resultValue!);
    }

    protected override GroupPath ParseLiteral(StringValueNode valueSyntax)
    {
        return new GroupPath(valueSyntax.Value);
    }

    protected override StringValueNode ParseValue(GroupPath runtimeValue)
    {
       return new StringValueNode(runtimeValue.ToString());
    }
}