/*
Author:durow
Date:2015.10.11
all ViewModel's base class.
Use MsgManager to send message.
Use UIDispatcher to cross to View's thread.
*/


using AyxMVVM.Message;
using AyxMVVM.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AyxMVVM
{
    public abstract class ViewModelBase : ObserveObject
    {
        private Frame _fame;
        /// <summary>
        /// Frame in the current window
        /// </summary>
        public Frame Frame
        {
            get
            {
                if (_fame == null)
                    _fame = Window.Current.Content as Frame;
                return _fame;
            }
            set { _fame = value; }
        }

        private CoreDispatcher _UIDispatcher;
        /// <summary>
        /// Current Frame's Dispatcher
        /// </summary>
        public CoreDispatcher UIDispatcher
        {
            internal set
            {
                _UIDispatcher = value;
            }
            get
            {
                if (_UIDispatcher == null)
                    _UIDispatcher = Frame.Dispatcher;
                return _UIDispatcher;
            }
        }

        private IMessageManager _msgManager;
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
        /// Used to init data for test
        /// </summary>
        public abstract void InitTestData();

        /// <summary>
        /// Used to init data for real
        /// </summary>
        public abstract void InitRealData();
    }
}
