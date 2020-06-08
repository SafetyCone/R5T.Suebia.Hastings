using System;

using R5T.Suebia.Alamania;
using R5T.Brighton;
using R5T.Hastings;
using R5T.Macommania;


namespace R5T.Suebia.Hastings
{
    public class MachineLocationAwareSecretsDirectoryPathProvider : ISecretsDirectoryPathProvider
    {
        private IExecutableFileDirectoryPathProvider ExecutableFileDirectoryPathProvider { get; }
        private IMachineLocationProvider MachineLocationProvider { get; }
        private IRivetOrganizationSecretsDirectoryPathProvider RivetOrganizationSecretsDirectoryPathProvider { get; }


        public MachineLocationAwareSecretsDirectoryPathProvider(
            IExecutableFileDirectoryPathProvider executableFileDirectoryPathProvider,
            IMachineLocationProvider machineLocationProvider,
            IRivetOrganizationSecretsDirectoryPathProvider rivetOrganizationSecretsDirectoryPathProvider)
        {
            this.ExecutableFileDirectoryPathProvider = executableFileDirectoryPathProvider;
            this.MachineLocationProvider = machineLocationProvider;
            this.RivetOrganizationSecretsDirectoryPathProvider = rivetOrganizationSecretsDirectoryPathProvider;
        }

        public string GetSecretsDirectoryPath()
        {
            var machineLocation = this.MachineLocationProvider.GetMachineLocation();

            string secretsDirectoryPath;
            switch(machineLocation.Value)
            {
                case Local.Name:
                    secretsDirectoryPath = this.GetLocalMachineLocationSecretsDirectoryPath();
                    break;

                case Remote.Name:
                    secretsDirectoryPath = this.GetRemoteMachineLocationSecretsDirectoryPath();
                    break;

                default:
                    throw new Exception($"Unrecognized machine location: '{machineLocation.Value}'.");
            }

            return secretsDirectoryPath;
        }

        /// <summary>
        /// For local machine locations (such as development PC), the Rivet/Data/Secrets directory (usually in Dropbox) is the custom user secrets directory.
        /// </summary>
        private string GetLocalMachineLocationSecretsDirectoryPath()
        {
            string rivetDataSecretsDirectoryPath = this.RivetOrganizationSecretsDirectoryPathProvider.GetSecretsDirectoryPath();

            var secretsDirectoryPath = rivetDataSecretsDirectoryPath;
            return secretsDirectoryPath;
        }

        /// <summary>
        /// For remote machine locations (code running on an AWS EC2 server instance for example), the executable file's directory is the custom user secrets directory.
        /// </summary>
        private string GetRemoteMachineLocationSecretsDirectoryPath()
        {
            string executableFileDirectoryPath = this.ExecutableFileDirectoryPathProvider.GetExecutableFileDirectoryPath();

            var secretsDirectoryPath = executableFileDirectoryPath;
            return secretsDirectoryPath;
        }
    }
}
