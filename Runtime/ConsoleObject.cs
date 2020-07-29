using System;

namespace Valax321.ConsoleCommands
{
    /// <summary>
    /// Base class for console commands and variables.
    /// </summary>
    public abstract class ConsoleObject
    {
        /// <summary>
        /// The name of this command/variable.
        /// </summary>
        public string name { get; }
        
        /// <summary>
        /// The description of this command/variable.
        /// </summary>
        public string description { get; }
        
        /// <summary>
        /// The string representation of the current value of this variable.
        /// </summary>
        public abstract string stringValue { get; }
        
        /// <summary>
        /// The flags set on this command/variable.
        /// </summary>
        public CommandFlags flags { get; }
        
        /// <summary>
        /// The number of parameters used by the command.
        /// </summary>
        public abstract int parameterCount { get; }

        /// <summary>
        /// The type of the parameters used in this object.
        /// </summary>
        public virtual string typeString { get; } = "None";

        /// <summary>
        /// Formatted object for writing to the config file.
        /// </summary>
        public virtual string configString { get; } = string.Empty;

        /// <summary>
        /// Instantiates a console object and registers it with the <see cref="CommandManager"/>.
        /// </summary>
        /// <param name="name">The name of this object.</param>
        /// <param name="description">The description of this object.</param>
        /// <param name="flags">The flags used for this object.</param>
        protected ConsoleObject(string name, string description, CommandFlags flags = CommandFlags.None)
        {
            this.name = name;
            this.description = description;
            this.flags = flags;
            
            CommandManager.RegisterObject(this);
        }

        /// <summary>
        /// Call the command, or set the value of the variable.
        /// </summary>
        /// <param name="args">List of arguments passed to this object when it is called.</param>
        public abstract void Call(params string[] args);

        /// <summary>
        /// Converts the given string to the specified type.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <typeparam name="T">The type to convert to.</typeparam>
        /// <returns>The parsed value.</returns>
        /// <exception cref="InvalidCastException">Thrown if value cannot be parsed successfully.</exception>
        public virtual T ParseValue<T>(string value) where T : IConvertible
        {
            try
            {
                return (T) Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                throw new InvalidCastException($"Could not convert {value} to type {typeof(T).FullName}");
            }
        }
    }
}