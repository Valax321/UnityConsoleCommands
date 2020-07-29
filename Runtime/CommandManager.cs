using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Valax321.ConsoleCommands
{
    /// <summary>
    /// Event for logging.
    /// </summary>
    /// <param name="message">Message info</param>
    public delegate void ConsoleLogEvent(ConsoleMessage message);
    
    /// <summary>
    /// Central class that manages the lifecycle of console commands.
    /// </summary>
    public static class CommandManager
    {
        private const char QuoteChar = '"';
        private const char ArgSeparatorChar = ',';
        private const char VarSetChar = '=';
        private const char CommentChar = '#';

        private static Dictionary<string, ConsoleObject> s_commands = new Dictionary<string, ConsoleObject>();
        
        /// <summary>
        /// Called when the <see cref="CommandManager"/> logs text.
        /// </summary>
        public static ConsoleLogEvent onMessageLogged { get; set; }

        /// <summary>
        /// Allows commands flagged as <see cref="CommandFlags"/>.DebugModeOnly to be executed.
        /// </summary>
        public static bool debugMode { get; set; } =
#if DEBUG
            true;
#else
            false;
#endif

        /// <summary>
        /// Allows commands flagged as <see cref="CommandFlags"/>.CheatModeOnly to be executed.
        /// </summary>
        public static bool cheatsMode { get; set; } = false;

        static CommandManager()
        {
            onMessageLogged += (message) =>
            {
                switch (message.type)
                {
                    case LogType.Log:
                        Debug.Log(message.message);
                        break;
                    case LogType.Warning:
                        Debug.Log(message.message);
                        break;
                    case LogType.Error:
                        Debug.LogError(message);
                        break;
                }
            };
        }
        
        /// <summary>
        /// Registers a console object.
        /// </summary>
        /// <param name="obj">The object to register.</param>
        /// <exception cref="DuplicateCommandException">Thrown if a duplicate command/variable is registered.</exception>
        public static void RegisterObject(ConsoleObject obj)
        {
            if (s_commands.ContainsKey(obj.name))
                throw new DuplicateCommandException(obj.name);

            s_commands.Add(obj.name, obj);
        }

        /// <summary>
        /// Gets the command with the given name.
        /// </summary>
        /// <param name="name">The name of the object to find.</param>
        /// <typeparam name="T">The type of console object to look for.</typeparam>
        /// <returns>The object if found, otherwise null.</returns>
        public static T Get<T>(string name) where T : ConsoleObject
        {
            if (s_commands.ContainsKey(name) && s_commands[name] is T)
            {
                return (T) s_commands[name];
            }

            return null;
        }

        /// <summary>
        /// Gets all registered and usable objects that contain the given string.
        /// </summary>
        /// <param name="search">The string to search for.</param>
        /// <returns><see cref="IEnumerable{T}"/> of all the command names found.</returns>
        public static IEnumerable<string> GetAutoCompleteForString(string search)
        {
            var items = s_commands.Keys.Where(x =>
            {
                bool has = x.Contains(search);

                if (!has)
                    return false;

                var flags = s_commands[x].flags;
                
                bool allowEditor = Application.isEditor || flags.HasFlag(CommandFlags.EditorOnly);
                bool allowDebug = debugMode || flags.HasFlag(CommandFlags.DebugModeOnly);
                bool allowCheats = cheatsMode || flags.HasFlag(CommandFlags.CheatModeOnly);

                return allowEditor && allowDebug && allowCheats;
            });

            return items;
        }

        public static void ExecuteString(string input)
        {
            
        }

        public static void Log(LogType type, string format, params object[] arguments)
        {
            onMessageLogged?.Invoke(new ConsoleMessage(string.Format(format, arguments), type));
        }
    }
}