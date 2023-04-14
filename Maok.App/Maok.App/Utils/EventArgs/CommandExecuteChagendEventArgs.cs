namespace Maok.App.Utils.EventArgs
{
    public class CommandExecuteChagendEventArgs : System.EventArgs
    {
        public bool InProgress { get; set; }

        public CommandExecuteChagendEventArgs(bool inProgress)
        {
            InProgress = inProgress;
        }
    }
}