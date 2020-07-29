namespace Valax321.ConsoleCommands
{
    /// <summary>
    /// Type of messaged log.
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// Normal Debug.Log
        /// </summary>
        Log,
        /// <summary>
        /// Debug.Warning
        /// </summary>
        Warning,
        /// <summary>
        /// Debug.Error
        /// </summary>
        Error
    }
    
    /// <summary>
    /// Pair of message text and the log type.
    /// </summary>
    public struct ConsoleMessage
    {
        /// <summary>
        /// The text of the message.
        /// </summary>
        public readonly string message;
        
        /// <summary>
        /// The type of message.
        /// </summary>
        public readonly LogType type;

        public ConsoleMessage(string message, LogType type)
        {
            this.message = message;
            this.type = type;
        }
    }
}