using System;

namespace Valax321.ConsoleCommands
{
    /// <summary>
    /// Flags used for commands and variables.
    /// </summary>
    [Flags]
    public enum CommandFlags
    {
        /// <summary>
        /// No flags
        /// </summary>
        None = 0,
        
        /// <summary>
        /// This command/variable can only be used when cheats are enabled.
        /// </summary>
        CheatModeOnly = 1 << 0,
        
        /// <summary>
        /// This command/variable can only be used when debug mode is enabled.
        /// </summary>
        DebugModeOnly = 1 << 1,
        
        /// <summary>
        /// This command/variable can only be used in the Unity Editor.
        /// </summary>
        EditorOnly = 1 << 2,
    }
}