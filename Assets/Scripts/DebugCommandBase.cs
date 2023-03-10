using System;

namespace CraftsmanHero {
    public class DebugCommandBase {
        public DebugCommandBase(string commandId, string commandDescription, string commandFormat) {
            CommandId = commandId;
            CommandDescription = commandDescription;
            CommandFormat = commandFormat;
        }

        public string CommandId { get; }
        public string CommandDescription { get; }
        public string CommandFormat { get; }

        public override string ToString() {
            return $"{CommandId} - {CommandFormat}: {CommandDescription}";
        }
    }

    public class DebugCommand : DebugCommandBase {
        readonly Action command;

        public DebugCommand(string commandId, string commandDescription, string commandFormat, Action command) : base(commandId, commandDescription, commandFormat) {
            this.command = command;
        }

        public void Invoke() {
            command.Invoke();
        }
    }

    public class DebugCommand<T1> : DebugCommandBase {
        readonly Action<T1> command;

        public DebugCommand(string commandId, string commandDescription, string commandFormat, Action<T1> command) : base(commandId, commandDescription, commandFormat) {
            this.command = command;
        }

        public void Invoke(T1 value) {
            command.Invoke(value);
        }
    }
}
