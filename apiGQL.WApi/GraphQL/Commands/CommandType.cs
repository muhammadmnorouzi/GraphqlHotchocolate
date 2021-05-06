using System.Linq;
using apiGQL.WApi.Data;
using apiGQL.WApi.Models;
using HotChocolate;
using HotChocolate.Types;

namespace apiGQL.WApi.GraphQL.Commands
{
    public class CommandType : ObjectType<Command>
    {
        protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
        {
            descriptor.Description("Represents all available commands for special platform.");

            descriptor
                .Field(p => p.Platform)
                .ResolveWith<Resolvers>(p => p.GetPlatforms(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("This is the platform that the command belongs to.");
        }

        private class Resolvers
        {
            public Platform GetPlatforms(Command command, [ScopedService] AppDbContext cotnext)
            {
                return cotnext.Platforms.FirstOrDefault(p => p.Id == command.PlatformId);
            }
        }
    }
}