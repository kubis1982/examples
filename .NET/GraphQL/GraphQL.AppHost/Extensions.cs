namespace GraphQL.AppHost
{
    using Aspire.Hosting.ApplicationModel;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal static class Extensions
    {
        public static IResourceBuilder<ProjectResource> WithGraphQLCommand(this IResourceBuilder<ProjectResource> builder)
        {
            builder.WithCommand(
                name: "run-graphql",
                displayName: "GraphQL",
                executeCommand: context => OnRunGraphQLCommandAsync(builder, context),
                updateState: OnUpdateResourceState,
                iconName: "AccessibilityCheckmarkFilled",
                iconVariant: IconVariant.Filled);

            return builder;
        }

        private static Task<ExecuteCommandResult> OnRunGraphQLCommandAsync(IResourceBuilder<ProjectResource> builder, ExecuteCommandContext context)
        {
            var url = "https://localhost:7046/GRAPHQL";
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });

            return Task.FromResult(CommandResults.Success());
        }

        private static ResourceCommandState OnUpdateResourceState(UpdateCommandStateContext context)
        {
            return context.ResourceSnapshot.HealthStatus is HealthStatus.Healthy
                ? ResourceCommandState.Enabled
                : ResourceCommandState.Disabled;
        }

        public static IResourceBuilder<ProjectResource> WithMigrateCommand(this IResourceBuilder<ProjectResource> builder)
        {
            builder.WithCommand(
                name: "run-migrate",
                displayName: "Migracja",
                executeCommand: context => OnRunMigrateCommandAsync(builder, context),
                updateState: OnUpdateResourceState,
                iconName: "AccessibilityCheckmarkFilled",
                iconVariant: IconVariant.Filled);

            return builder;
        }

        private static async Task<ExecuteCommandResult> OnRunMigrateCommandAsync(IResourceBuilder<ProjectResource> builder, ExecuteCommandContext context)
        {
            var url = "https://localhost:7046/migration";
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine("Błąd: " + response.StatusCode);
            }

            return CommandResults.Success();
        }
    }
}
