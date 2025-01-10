using System;
using System.Windows.Input;

namespace Shopfloor.Shared.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        // protected bool IsExecuting { get; set; }
        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }
        public abstract void Execute(object? parameter);
        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}