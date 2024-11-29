using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private Stack<ICommand> commandHistory = new Stack<ICommand> ();
    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        commandHistory.Push(command);
    }
    public void UndoCommand()
    {
        if(commandHistory.Count > 0)
        {
            ICommand lastCommand = commandHistory.Pop();
            lastCommand.Undo();
        }
    }
}
