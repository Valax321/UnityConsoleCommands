using System;

namespace Valax321.ConsoleCommands
{
    public abstract class ConsoleVariableBase<T> : ConsoleObject
    {
        /// <inheritdoc />
        public override string stringValue => DefaultValue.ToString();
        
        /// <inheritdoc />
        public override int parameterCount { get; } = 1;
        
        /// <summary>
        /// Instantiates and registers a new console variable.
        /// </summary>
        /// <param name="defaultValue">The default value for this variable.</param>
        /// <param name="name">The name of this variable.</param>
        /// <param name="description">The description of this variable.</param>
        /// <param name="flags">The flags used by this variable.</param>
        protected ConsoleVariableBase(T defaultValue, string name, string description, CommandFlags flags = CommandFlags.None) : base(name, description, flags)
        {
            this.DefaultValue = defaultValue;
            this.defaultValue = defaultValue;
        }

        /// <summary>
        /// The value of the console variable.
        /// </summary>
        public T DefaultValue { get; set; }
        
        /// <summary>
        /// The default value of the console variable.
        /// </summary>
        public T defaultValue { get; }
        
        protected virtual void ValueChanged(T newValue) { }

        public void ResetToDefault()
        {
            if (!DefaultValue.Equals(defaultValue))
            {
                DefaultValue = defaultValue;
                ValueChanged(defaultValue);
            }
        }
    }
    
    /// <summary>
    /// Console variable. Can be saved to config files etc.
    /// </summary>
    /// <typeparam name="T">The type of the variable.</typeparam>
    public class ConsoleVariable<T> : ConsoleVariableBase<T> where T: IConvertible
    {
        /// <inheritdoc />
        public ConsoleVariable(string name, T defaultValue, string description, CommandFlags flags = CommandFlags.None)
            : base(defaultValue, name, description, flags)
        {
        }
        
        /// <inheritdoc cref="ConsoleObject.Call"/>
        public override void Call(params string[] args)
        {
            if (args.Length < 1)
            {
                CommandManager.Log(LogType.Error, "{0} must take 1 argument", name);
                return;
            }

            try
            {
                var v = ParseValue<T>(args[0]);
                if (!DefaultValue.Equals(v))
                {
                    DefaultValue = v;
                    ValueChanged(v);
                }
            }
            catch (Exception ex)
            {
                CommandManager.Log(LogType.Error, "{0}: {1}", name, ex.Message);
            }
        }
    }

    /// <summary>
    /// Console variable with a callback. The callback is invoked when the value of the variable changes.
    /// </summary>
    /// <typeparam name="T">The type of the variable.</typeparam>
    public class ConsoleCallbackVariable<T> : ConsoleVariable<T> where T : IConvertible
    {
        /// <summary>
        /// The callback invoked when the value is changed.
        /// </summary>
        public Action<T> callback { get; }

        /// <summary>
        /// Instantiates and registers a new console variable with a callback.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="defaultValue">The default value of the variable.</param>
        /// <param name="callback">The function called when the value of the variable changes.</param>
        /// <param name="description">The description of the variable.</param>
        /// <param name="flags">The flags used by the variable.</param>
        public ConsoleCallbackVariable(string name, T defaultValue, Action<T> callback, string description,
            CommandFlags flags = CommandFlags.None) : base(name, defaultValue, description, flags)
        {
            this.callback = callback;
        }

        protected override void ValueChanged(T newValue)
        {
            callback.Invoke(newValue);
        }
    }

    /// <summary>
    /// A console variable supporting enums via their string names.
    /// </summary>
    /// <typeparam name="T">The enum type for this variable.</typeparam>
    public class ConsoleEnumVariable<T> : ConsoleVariableBase<T> where T: struct, Enum
    {
        /// <inheritdoc />
        public ConsoleEnumVariable(string name, T defaultValue, string description, CommandFlags flags = CommandFlags.None) 
            : base(defaultValue, name, description, flags)
        {
        }
        
        public override void Call(params string[] args)
        {
            if (args.Length < 1)
            {
                CommandManager.Log(LogType.Error, "{0} must take 1 argument", name);
                return;
            }

            if (Enum.TryParse(args[0], out T result))
            {
                if (!DefaultValue.Equals(result))
                {
                    DefaultValue = result;
                    ValueChanged(result);
                }
            }
            else
            {
                CommandManager.Log(LogType.Error, "{0}: {1} is not a valid value for {2}", name, args[0], typeof(T).FullName);
            }
        }
    }

    /// <inheritdoc cref="ConsoleCallbackVariable{T}"/>
    public class ConsoleEnumCallbackVariable<T> : ConsoleEnumVariable<T> where T : struct, Enum
    {
        public Action<T> callback { get; }

        /// <summary>
        /// Instantiates and registers a new enum console variable with a callback.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="defaultValue">The default value of the variable.</param>
        /// <param name="callback">The function called when the value of the variable changes.</param>
        /// <param name="description">The description of the variable.</param>
        /// <param name="flags">The flags used by the variable.</param>
        public ConsoleEnumCallbackVariable(string name, T defaultValue, Action<T> callback, string description, CommandFlags flags = CommandFlags.None) 
            : base(name, defaultValue, description, flags)
        {
            this.callback = callback;
        }

        protected override void ValueChanged(T newValue)
        {
            callback.Invoke(newValue);
        }
    }
}