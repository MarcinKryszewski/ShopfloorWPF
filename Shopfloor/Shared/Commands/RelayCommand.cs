﻿using System;
using System.Windows.Input;

namespace Shopfloor.Shared.Commands
{
    internal class RelayCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        public bool CanExecute(object? parameter) => _canExecute(parameter ?? true);
        public void Execute(object? parameter) => _execute(parameter ?? true);
    }
}