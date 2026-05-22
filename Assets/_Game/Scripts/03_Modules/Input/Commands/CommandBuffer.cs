using System.Collections.Generic;
using BillGameCore.SharedPorts.Input;

namespace BillGameCore.Modules.Input.Commands
{
    public sealed class CommandBuffer
    {
        private readonly Queue<ICommand> _queue;

        public CommandBuffer(int capacity)
        {
            _queue = new Queue<ICommand>(capacity);
        }
        public void Enqueue(ICommand command)
        {
            _queue.Enqueue(command);
        }

        public bool TryDequeue(out ICommand command)
        {
            if (_queue.Count > 0)
            {
                command = _queue.Dequeue();
                return true;
            }

            command = default;
            return false;
        }

        public void Clear()
        {
            _queue.Clear();
        }
    }
}