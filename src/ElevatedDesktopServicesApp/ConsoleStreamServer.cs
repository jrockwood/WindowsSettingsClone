// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleStreamServer.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ElevatedDesktopServicesApp
{
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Logging;
    using Shared.CommandBridge;
    using Shared.Diagnostics;
    using Shared.Logging;
    using SharedWin32.CommandExecutors;
    using SharedWin32.CommandExecutors.Registry;

    /// <summary>
    /// Function definition for reading a line from the underlying stream. Useful for unit tests.
    /// </summary>
    public delegate string ReadLineFunc();

    /// <summary>
    /// Function definition for writing a line to a stream. Useful for unit tests.
    /// </summary>
    /// <param name="value"></param>
    public delegate void WriteLineAction(string value);

    /// <summary>
    /// Handles incoming requests that come across a redirected standard input stream and sends responses back on the
    /// standard output stream. Errors are sent across the standard output stream, but a terminating exception is sent on
    /// the standard error stream.
    /// </summary>
    public sealed class ConsoleStreamServer
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly ILogger _logger;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public ConsoleStreamServer(ILogger logger)
        {
            _logger = Param.VerifyNotNull(logger, nameof(logger));
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        /// <summary>
        /// Starts the server by waiting for requests (via the <paramref name="readLineFunc"/>) and then sending
        /// responses via <paramref name="writeLineAction"/>.
        /// </summary>
        /// <param name="readLineFunc">
        /// The function to call to wait for an incoming command, serialized on a single line.
        /// </param>
        /// <param name="writeLineAction">
        /// The action to call when a response is sent back to the client, serialized on a singe line.
        /// </param>
        /// <param name="registry">A <see cref="IWin32Registry"/> to use for executing registry commands.</param>
        public void Start(ReadLineFunc readLineFunc, WriteLineAction writeLineAction, IWin32Registry registry)
        {
            Param.VerifyNotNull(readLineFunc, nameof(readLineFunc));
            Param.VerifyNotNull(writeLineAction, nameof(writeLineAction));
            Param.VerifyNotNull(registry, nameof(registry));

            var commandExecutor = new CommandExecutor(_logger, registry);

            // This is the loop that waits for an incoming request (ReadLine), executes a command, and then returns a
            // response via WriteLine.
            bool shutdownRequested = false;

            while (!shutdownRequested)
            {
                _logger.LogDebug("Waiting for request");
                string serializedCommand = readLineFunc();
                _logger.LogInfo("Request received: ", serializedCommand);

                IServiceCommandResponse response;

                if (!ServiceCommand.TryDeserializeFromJsonString(
                    serializedCommand,
                    out IServiceCommand command,
                    out IServiceCommandResponse errorResponse))
                {
                    response = errorResponse;
                }
                else
                {
                    _logger.LogDebug("Executing command: ", command.ToDebugString());
                    shutdownRequested = command.CommandName == ServiceCommandName.ShutdownServer;
                    response = commandExecutor.Execute(command);
                }

                string responseMessage = response.SerializeToJsonString();
                _logger.LogInfo("Sending response: ", response.ToDebugString());
                writeLineAction(responseMessage);
            }
        }
    }
}
