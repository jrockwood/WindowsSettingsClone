// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ElevatedAppCommunicationBridge.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.DesktopServicesApp
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Logging;
    using Shared.CommandBridge;
    using Shared.Logging;

    /// <summary>
    /// Communication bridge between the full-trust application (this) and the elevated process (ElevatedDesktopServicesApp).
    /// </summary>
    internal sealed class ElevatedAppCommunicationBridge
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        // HRESULT 80004005 is E_FAIL
        // ReSharper disable once InconsistentNaming
        private const int E_FAIL = unchecked((int)0x80004005);

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public async Task<IServiceCommandResponse> SendCommandAsync(IServiceCommand command, ILogger logger)
        {
            // The path to the elevated app is a zero-byte executable EXE that facilitates the packaged app launch for
            // classic invokers – such as CMD or Process.Start.
            string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string elevatedAppPath = Path.Combine(
                localAppDataPath,
                "microsoft",
                "windowsapps",
                "ElevatedDesktopServicesApp.exe");

            string args = BuildArgs(command);

            var processStartInfo = new ProcessStartInfo
            {
                Arguments = args,
                FileName = elevatedAppPath,
                Verb = "runas",
                UseShellExecute = true
            };

            int exitCode = 0;

            try
            {
                var elevatedProcess = Process.Start(processStartInfo);
                elevatedProcess?.WaitForExit(5000);
                exitCode = elevatedProcess?.ExitCode ?? 3;
            }
            catch (Exception e)
            {
                if (e.HResult == E_FAIL)
                {
                    // the user cancelled the elevated process
                    // by clicking "No" on the Windows elevation dialog
                    exitCode = 1;
                }

                logger.LogError($"Error starting elevated process: {e}");
            }

            ServiceCommandResponse response = exitCode == 0
                ? ServiceCommandResponse.Create(command.CommandName, 0)
                : ServiceCommandResponse.CreateError(
                    command.CommandName,
                    new InvalidOperationException($"Error starting elevated process: {exitCode}"));

            return await Task.FromResult(response);
        }

        private string BuildArgs(IServiceCommand command)
        {
            var builder = new StringBuilder();

            builder.Append("/attachDebugger");

            return builder.ToString();
        }
    }
}
