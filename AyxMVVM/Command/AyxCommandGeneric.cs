/*
Author:durow
Date:2015.10.11
Binding command generic
*/
using System;
using System.Windows.Input;

namespace AyxMVVM.Command
{
    public class AyxCommand<T>:ICommand
    {
        /// <summary>
        /// Binding command generic
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        /// <summary>
        /// The function that check if the command can execute
        /// </summary>
        private Func<T, bool> _canExecute;

        /// <summary>
        /// The action that the command execute
        /// </summary>
        private Action<T> _execute;

        /// <summary>
        /// Create a new AyxCommand instance
        /// </summary>
        /// <param name="execute">The action that the command execute</param>
        public AyxCommand(Action<T> execute):this(execute, null)
        {
        }

        /// <summary>
        /// Create a new AyxCommand instance
        /// </summary>
        /// <param name="execute">The action that the command execute</param>
        /// <param name="canExecute">The function that check if the command can execute</param>
        public AyxCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Method that check if the command can execute
        /// </summary>
        /// <param name="parameter">Command's parameter</param>
        /// <returns>if the command can execute</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null) return true;
            return _canExecute((T)parameter);
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (_execute != null && CanExecute(parameter))
            {
                _execute((T)parameter);
            }
        }
    }
}
