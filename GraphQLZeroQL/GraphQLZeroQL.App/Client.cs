using GraphQL.TestServer;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GraphQLZeroQL.App
{
    public class Client
    {
        public static HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:10000/graphql")
        };

        public static TestServerClient client = new TestServerClient(httpClient);

        public async Task StandardQuery()
        {
            var response = await client.Query(static q => q.Me(o => new { o.Id, o.FirstName, o.LastName }));

            Console.WriteLine($"GraphQL: {response.Query}");
            // GraphQL: query { me { id firstName lastName } }
            Console.WriteLine($"{response.Data.Id}: {response.Data.FirstName} {response.Data.LastName}"); // 1: Jon Smith
        }


        public async Task DeeplyNestedQuery()
        {
            var variables = new { Id = 1 };
            var response = await client.Query(
                variables,
                static (i, q) => q
                    .User(i.Id,
                        o => new
                        {
                            o.Id,
                            o.FirstName,
                            o.LastName,
                            Role = o.Role(role => role.Name)
                        }));

            Console.WriteLine($"GraphQL: {response.Query}");
            // GraphQL: query GetUserWithRole($id: Int!) { user(id: $id) { id firstName lastName role { name }  } }
            Console.WriteLine($"{response.Data.Id}: {response.Data.FirstName} {response.Data.LastName}, Role: {response.Data.Role}");
            // 1: Jon Smith, Role: Admin
        }

        public async Task MultipleFieldsQuery()
        {
            var variables = new { Id = 1 };
            var response = await client.Query(
                variables,
                static (i, q) => new
                {
                    MyFirstName = q.Me(o => o.FirstName),
                    User = q.User(i.Id,
                        o => new
                        {
                            o.FirstName,
                            o.LastName,
                            Role = o.Role(role => role.Name)
                        })
                });

            Console.WriteLine($"GraphQL: {response.Query}");
            // GraphQL: query GetUserWithRole($id: Int!) { me { firstName }  user(id: $id) { firstName lastName role { name }  } }
            Console.WriteLine($"Me: {response.Data.MyFirstName}, User: {response.Data.User.FirstName} {response.Data.User.LastName}, Role: {response.Data.User.Role}");
            // Me: Jon, User: Jon Smith, Role: Admin
        }

        public async Task ExecuteMutation()
        {
            var response = await client.Mutation(m => m.AddUser("Jon", "Doe", o => o.Id));

            Console.WriteLine($"GraphQL: {response.Query}");
            Console.WriteLine($"Id: {response.Data}");
        }
    }
}