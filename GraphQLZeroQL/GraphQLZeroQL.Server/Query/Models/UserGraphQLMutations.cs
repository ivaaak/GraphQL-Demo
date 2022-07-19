using GraphQLZeroQL.Server.Query;

namespace GraphQLZeroQL.Server.Query.Models;

[ExtendObjectType(typeof(Mutation))]
public class UserGraphQLMutations
{
    public User AddUser(string firstName, string lastName)
    {
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName
        };

        return user;
    }
}