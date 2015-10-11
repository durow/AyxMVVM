/*
Author:durow
Date:2015.10.11
all ViewModel's base class.
Use MsgManager to send message.
Use UIDispatcher to cross to View's thread.
*/
using System.Windows.Threading;
using AyxMVVM.Message;
using AyxMVVM.Threading;


namespace AyxMVVM
{
    public class ViewModelBase : ObserveObject
    {
        private IMessageManager _msgManager;
        private Dispatcher _UIDispatcher;

        /// <summary>
        /// Used to send message to View
        /// </summary>
        public IMessageManager MsgManager
        {
            get
            {
                if (_msgManager == null)
                    _msgManager = MessageManager.Default;
                return _msgManager;
            }
            set { _msgManager = value; }
        }

        /// <summary>
        /// Used to cross to the View's thread
        /// </summary>
        public Dispatcher UIDispatcher
        {
            get
            {
                if (_UIDispatcher == null)
                    _UIDispatcher = DispatcherHelper.UIDispatcher;
                return _UIDispatcher;
            }
            set { _UIDispatcher = value; }
        }

    }
}
