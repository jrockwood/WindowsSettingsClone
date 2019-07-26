// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="FileCommandExecutor.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32.CommandExecutors.IO
{
    using System;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Commands;
    using ServiceContracts.Logging;
    using Shared.CommandBridge;
    using Shared.Logging;

    public sealed class FileCommandExecutor
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly ILogger _logger;
        private readonly IWin32FileSystem _fileSystem;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCommandExecutor"/> class using the specified file system.
        /// </summary>
        /// <param name="fileSystem">The file system to use. If not supplied, the OS file system will be used.</param>
        /// <param name="logger">The logger to use.</param>
        public FileCommandExecutor(IWin32FileSystem fileSystem = null, ILogger logger = null)
        {
            _fileSystem = fileSystem ?? new Win32FileSystem();
            _logger = logger ?? new NullLogger();
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public IServiceCommandResponse Execute(IFileCopyCommand command)
        {
            IServiceCommandResponse response;

            try
            {
                _fileSystem.CopyFile(command.SourcePath, command.DestinationPath, command.Overwrite);
                response = ServiceCommandResponse.Create(command.CommandName, true);
            }
            catch (Exception e)
            {
                response = ServiceCommandResponse.CreateError(command.CommandName, e);
            }

            return response;
        }
    }
}
