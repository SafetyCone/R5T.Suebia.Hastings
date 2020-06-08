using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Dacia;
using R5T.Hastings;
using R5T.Macommania;
using R5T.Suebia.Alamania;


namespace R5T.Suebia.Hastings
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="MachineLocationAwareSecretsDirectoryPathProvider"/> implementation of <see cref="ISecretsDirectoryPathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddMachineLocationAwareSecretsDirectoryPathProvider(this IServiceCollection services,
            IServiceAction<IExecutableFileDirectoryPathProvider> executableFileDirectoryPathProviderAction,
            IServiceAction<IMachineLocationProvider> machineLocationProviderAction,
            IServiceAction<IRivetOrganizationSecretsDirectoryPathProvider> rivetOrganizationSecretsDirectoryPathProviderAction)
        {
            services
                .AddSingleton<ISecretsDirectoryPathProvider, MachineLocationAwareSecretsDirectoryPathProvider>()
                .Run(executableFileDirectoryPathProviderAction)
                .Run(machineLocationProviderAction)
                .Run(rivetOrganizationSecretsDirectoryPathProviderAction)
                ;

            return services;
        }

        /// <summary>
        /// Adds the <see cref="MachineLocationAwareSecretsDirectoryPathProvider"/> implementation of <see cref="ISecretsDirectoryPathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<ISecretsDirectoryPathProvider> AddMachineLocationAwareSecretsDirectoryPathProviderAction(this IServiceCollection services,
            IServiceAction<IExecutableFileDirectoryPathProvider> executableFileDirectoryPathProviderAction,
            IServiceAction<IMachineLocationProvider> machineLocationProviderAction,
            IServiceAction<IRivetOrganizationSecretsDirectoryPathProvider> rivetOrganizationSecretsDirectoryPathProviderAction)
        {
            var serviceAction = ServiceAction<ISecretsDirectoryPathProvider>.New(() => services.AddMachineLocationAwareSecretsDirectoryPathProvider(
                executableFileDirectoryPathProviderAction,
                machineLocationProviderAction,
                rivetOrganizationSecretsDirectoryPathProviderAction));

            return serviceAction;
        }
    }
}
