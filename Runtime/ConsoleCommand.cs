using System;

namespace Valax321.ConsoleCommands
{
    public interface IConsoleCommand
    {
        
    }
    
    /// <summary>
    /// Console command with no parameters.
    /// </summary>
    public class ConsoleCommand : ConsoleObject, IConsoleCommand
    {
        /// <summary>
        /// The function bound to this command.
        /// </summary>
        public Action function { get; }

        public override string stringValue => function.Method.Name;
        
        public override int parameterCount { get; } = 0;

        /// <summary>
        /// Instantiates and registers a console command with no parameters.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="function">The function bound to this command.</param>
        /// <param name="description">The description of this command.</param>
        /// <param name="flags">The flags for this command.</param>
        public ConsoleCommand(string name, Action function, string description, CommandFlags flags = CommandFlags.None) : base(name, description, flags)
        {
            this.function = function;
        }

        public override void Call(params string[] args)
        {
            function.Invoke();
        }
    }

    /// <summary>
    /// Console command with 1 parameter.
    /// </summary>
    /// <typeparam name="T1">The first argument.</typeparam>
    public class ConsoleCommand<T1> : ConsoleObject, IConsoleCommand where T1: IConvertible
    {
        /// <summary>
        /// The function bound to this command.
        /// </summary>
        public Action<T1> function { get; }

        public override string stringValue => function.Method.Name;

        public override int parameterCount { get; } = 1;

        public ConsoleCommand(string name, Action<T1> function, string description,
            CommandFlags flags = CommandFlags.None) : base(name, description, flags)
        {
            this.function = function;
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
                var param1 = ParseValue<T1>(args[0]);
                function.Invoke(param1);
            }
            catch (Exception ex)
            {
                CommandManager.Log(LogType.Error, "{0} Error: {1}", name, ex.Message);
            }
        }
    }
}