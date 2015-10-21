/*
Author:durow
Date:2015.10.11
Binding command
*/
using System;
using System.Threading.Tasks;
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
        private Func<bool> _canExecute;

        /// <summary>
        /// The action that the command execute
        /// </summary>
        private Func<Task> _execute;

        /// <summary>
        /// Create a new AyxCommand instance
        /// </summary>
        /// <param name="execute">The action that the command execute</param>
        public AyxCommand(Action execute):this(execute,()=>true)
        {
        }

        /// <summary>
        /// Create a new AyxCommand instance
        /// </summary>
        /// <param name="execute">The action that the command execute</param>
        /// <param name="canExecute">The function that check if the command can execute</param>
        public AyxCommand(Action execute, Func<bool> canExecute)
        {
            _execute = () => { execute(); return Task.Delay(0); };
            _canExecute = canExecute;
        }

        public AyxCommand(Func<Task> asyncExecute)
            :this(asyncExecute,()=>true)
        { }

        public AyxCommand(Func<Task> asyncExecute,Func<bool> canExecute)
        {
            _execute = asyncExecute;
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
            return _canExecute();
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">Command's parameter</param>
        public async void Execute(object parameter)
        {
            if (_execute != null && CanExecute(parameter))
            {
                await _execute();
            }
        }

        /// <summary>
        /// Check if the command can execute
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

    }
}
