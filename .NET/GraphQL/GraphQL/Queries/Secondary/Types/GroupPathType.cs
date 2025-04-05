namespace GraphQL.Queries.Secondary.Types;
using GraphQL.Queries.Primary.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class GroupPathType : ObjectType<GroupPath>
{
    protected override void Configure(IObjectTypeDescriptor<GroupPath> descriptor)
    {
        descriptor.Name(nameof(GroupPathType));
        descriptor.BindFieldsExplicitly();

        descriptor
            .Field("text")
            .Resolve(ctx => ctx.Parent<GroupPath>().ToString())
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(n => n.NLevel)
            .Type<NonNullType<IntType>>();
    }
}