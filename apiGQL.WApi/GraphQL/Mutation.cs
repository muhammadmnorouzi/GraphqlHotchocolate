using System.Threading;
using System.Threading.Tasks;
using apiGQL.WApi.Data;
using apiGQL.WApi.GraphQL.Commands;
using apiGQL.WApi.GraphQL.Platforms;
using apiGQL.WApi.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;

namespace apiGQL.WApi.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input, [ScopedService] AppDbContext context, [Service] ITopicEventSender eventSender, CancellationToken cancellationToken)
        {
            var platform = new Platform
            {
                Name = input.Name
            };
            await context.Platforms.AddAsync(platform);
            await context.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform, cancellationToken);

            return new AddPlatformPayload(platform);
        }

        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService] AppDbContext context)
        {
            var command = new Command
            {
                HowTo = input.HowTo,
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId
            };

            await context.Commands.AddAsync(command);
            await context.SaveChangesAsync();

            return new AddCommandPayload(command);
        }
    }
}