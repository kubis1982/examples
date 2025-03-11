//using HotChocolate.Language;
//using Microsoft.EntityFrameworkCore;

//namespace GraphQL.GraphQLs
//{
//    public class LTreeType : ScalarType<LTree, StringValueNode> {
//        public LTreeType() : base("LTree", BindingBehavior.Implicit) {
//        }

//        public override IValueNode ParseResult(object? resultValue) {
//            return new StringValueNode(resultValue?.ToString() ?? string.Empty);
//        }

//        protected override LTree ParseLiteral(StringValueNode valueSyntax) {
//            return new LTree(valueSyntax.Value);
//        }

//        protected override StringValueNode ParseValue(LTree runtimeValue) {
//            return new StringValueNode(runtimeValue);
//        }
//    }
//}
