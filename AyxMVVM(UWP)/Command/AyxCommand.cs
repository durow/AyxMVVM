/*
Author:durow
Date:2015.10.11
Binding command
*/
using System;
using System.Windows.Input;

namespace AyxMVVM.Command
{
    public class AyxCommand:ICommand
    {
        /// <summary>
        /// Check if the command can execute
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// The function that check if the command can execute
        /// </summary>
        private Func<object, bool> _canExecute;

        /// <summary>
        /// The action that the command execute
        /// </summary>
        private Action<object> _execute;

        /// <summary>
        /// Create a new AyxCommand instance
        /// </summary>
        /// <param name="execute">The action that the command execute</param>
        public AyxCommand(Action<object> execute):this(execute,null)
        {
        }

        /// <summary>
        /// Create a new AyxCommand instance
        /// </summary>
        /// <param name="execute">The action that the command execute</param>
        /// <param name="canExecute">The function that check if the command can execute</param>
        public AyxCommand(Action<object> execute, Func<object, bool> canExecute)
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
            return _canExecute(parameter);
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">Command's parameter</param>
        public void Execute(object parameter)
        {
            if (_execute != null && CanExecute(parameter))
            {
                _execute(parameter);
            }
        }
    }
}
