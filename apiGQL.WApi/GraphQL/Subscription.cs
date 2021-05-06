using apiGQL.WApi.Models;
using HotChocolate;
using HotChocolate.Types;

namespace apiGQL.WApi.GraphQL
{
    public class Subscription
    {
        [Subscribe]
        public Platform OnPlatformAdded([EventMessage] Platform platform)
        {
            return platform;
        }
    }
}