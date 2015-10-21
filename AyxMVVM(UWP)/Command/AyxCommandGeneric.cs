/*
Author:durow
Date:2015.10.11
Binding command generic
*/
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AyxMVVM.Command
{
    public class AyxCommand<T>:ICommand
    {
        /// <summary>
        /// Binding command generic
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// The function that check if the command can execute
        /// </summary>
        private Func<T, bool> _canExecute;

        /// <summary>
        /// The action that the command execute
        /// </summary>
        private Func<T, Task> _execute;

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
            _execute = (t) => { execute(t); return Task.Delay(0); };
            _canExecute = canExecute;
        }

        public AyxCommand(Func<T,Task> asyncExecute)
            :this(asyncExecute,(t)=>true)
        { }

        public AyxCommand(Func<T,Task> asyncExecute,Func<T,bool> canExecute)
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
            return _canExecute((T)parameter);
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter"></param>
        public async void Execute(object parameter)
        {
            if (_execute != null && CanExecute(parameter))
            {
                await _execute((T)parameter);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}
