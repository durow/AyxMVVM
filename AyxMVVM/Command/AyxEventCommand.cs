/*
Author:durow
Date:2015.10.11
Use this class to bind event to command
*/
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace AyxMVVM.Command
{
    public class MyEventCommand : TriggerAction<DependencyObject>
    {

        /// <summary>
        /// Command to bind
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MsgName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(MyEventCommand), new PropertyMetadata(null));

        /// <summary>
        /// Command parameter
        /// </summary>
        public object CommandParateter
        {
            get { return (object)GetValue(CommandParateterProperty); }
            set { SetValue(CommandParateterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandParateter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParateterProperty =
            DependencyProperty.Register("CommandParateter", typeof(object), typeof(MyEventCommand), new PropertyMetadata(null));

        protected override void Invoke(object parameter)
        {
            if (CommandParateter != null)
                parameter = CommandParateter;
            var cmd = GetCommand();
            if (cmd != null)
                cmd.Execute(parameter);
        }

        private ICommand GetCommand()
        {
            return Command;
        }
    }
}
