﻿using System;
using System.Threading.Tasks;

namespace Shopfloor.Shared.Commands
{
    public abstract class AsyncCommandBase : CommandBase
    {
        private readonly Action<Exception>? _onException;

        private bool _isExecuting;

        protected AsyncCommandBase(Action<Exception>? onException = null)
        {
            _onException = onException;
        }
        public bool IsExecuting
        {
            get
            {
                return _isExecuting;
            }
            private set
            {
                _isExecuting = value;
                OnCanExecuteChanged();
            }
        }
        public override bool CanExecute(object? parameter)
        {
            return !IsExecuting && base.CanExecute(parameter);
        }

        public override async void Execute(object? parameter)
        {
            IsExecuting = true;

            try
            {
                //await ExecuteAsync(parameter);
                await Task.Run(async () =>
                {
                    await ExecuteAsync(parameter);
                });
            }
            catch (Exception ex)
            {
                _onException?.Invoke(ex);
            }

            IsExecuting = false;
        }

        public abstract Task ExecuteAsync(object? parameter);
    }
}