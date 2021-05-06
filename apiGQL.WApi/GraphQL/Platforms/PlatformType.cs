using System.Linq;
using apiGQL.WApi.Data;
using apiGQL.WApi.Models;
using HotChocolate;
using HotChocolate.Types;

namespace apiGQL.WApi.GraphQL.Platforms
{
    public class PlatformType : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor.Description("Represents any software or service thar has a cli.");
            descriptor.Field(p => p.LicenseKey).Ignore();

            descriptor
                .Field(p => p.Commands)
                .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("List of commands for this platform.");
        }

        private class Resolvers
        {
            public IQueryable<Command> GetCommands(Platform platform, [ScopedService] AppDbContext cotnext)
            {
                return cotnext.Commands.Where(c => c.PlatformId == platform.Id);
            }
        }
    }
}