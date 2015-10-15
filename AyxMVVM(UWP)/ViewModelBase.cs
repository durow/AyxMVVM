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

namespace AyxMVVM
{
    public class ViewModelBase : ObserveObject
    {
        private CoreDispatcher _UIDispatcher;
        public CoreDispatcher UIDispatcher
        {
            internal set
            {
                _UIDispatcher = value;
            }
            get
            {
                if (_UIDispatcher == null)
                    _UIDispatcher = DispatcherHelper.UIDispatcher;
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


    }
}
