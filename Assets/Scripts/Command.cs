using System.Collections.Generic;

public static class Command
{
    public interface ICommand
    {
        public void Execute();
        public void Undo();
    }

    private static Stack<ICommand> _undoStack = new();
    private static Queue<ICommand> _redoQueue = new();

    public static void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _undoStack.Push(command);
    }

    public static void UndoCommand()
    {
        if (_undoStack.Count > 0)
        {
            ICommand activeCommand = _undoStack.Pop();
            activeCommand.Undo();
            
            _redoQueue.Enqueue(activeCommand);
        }
    }

    public static void RedoCommand()
    {
        if (_redoQueue.Count > 0)
        {
            ICommand activeCommand = _redoQueue.Dequeue();
            activeCommand.Execute();
        }
    }

    // In case a new command is invoked, clear the undo queue
    public static void ClearRedoBuffer()
    {
        _redoQueue.Clear();
    }
}
