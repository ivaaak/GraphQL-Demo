using System;
using System.Net.Http;
using System.Threading.Tasks;
using GraphQL.TestServer;
using ZeroQL.Core;

namespace GraphQLZeroQL.App
{
    public class Program
    {
        private static HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:10000/graphql")
        };
        private static TestServerClient qlClient = new TestServerClient(httpClient);



        public static async Task Main()
        {
            await Execute();
        }

        public static async Task<IGraphQLResult> Execute()
        {
            // place to replace
            var response = await qlClient.Query(static q => q.Me(o => o.FirstName));

            return response;
        }
    }

    public record User(string FirstName, string LastName, string Role);
}