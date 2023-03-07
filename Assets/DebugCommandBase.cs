using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero {
    public class DebugCommandBase {
        public string CommandId { get; }
        public string CommandDescription { get; }
        public string CommandFormat { get; }

        public DebugCommandBase(string commandId, string commandDescription, string commandFormat) {
            CommandId = commandId;
            CommandDescription = commandDescription;
            CommandFormat = commandFormat;
        }
    }

    public class DebugCommand : DebugCommandBase {
        Action command;

        public DebugCommand(string commandId, string commandDescription, string commandFormat, Action command) : base(commandId, commandDescription, commandFormat) {
            this.command = command;
        }

        public void Invoke() {
            command.Invoke();
        }
    }

    public class DebugCommand<T1> : DebugCommandBase {
        Action<T1> command;

        public DebugCommand(string commandId, string commandDescription, string commandFormat, Action<T1> command) : base(commandId, commandDescription, commandFormat) {
            this.command = command;
        }

        public void Invoke(T1 value) {
            command.Invoke(value);
        }
    }

    public class DebugCommand<T1, T2> : DebugCommandBase {
        Action<T1, T2> command;

        public DebugCommand(string commandId, string commandDescription, string commandFormat, Action<T1, T2> command) : base(commandId, commandDescription, commandFormat) {
            this.command = command;
        }

        public void Invoke(T1 value, T2 value2) {
            command.Invoke(value, value2);
        }
    }
}
