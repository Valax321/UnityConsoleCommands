using System;

namespace Valax321.ConsoleCommands
{
    /// <summary>
    /// Exception thrown by <see cref="CommandManager"/> when two commands with the same name are registered.
    /// </summary>
    public class DuplicateCommandException : Exception
    {
        private readonly string commandName;
        
        public DuplicateCommandException(string commandName)
        {
            this.commandName = commandName;
        }

        public override string Message => $"Duplicate command named {commandName} was registered.";
    }
}