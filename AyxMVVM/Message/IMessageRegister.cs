using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AyxMVVM.Message
{
    public interface IMessageRegister
    {
        object RegInstance { get; set; }
        IMessageManager MsgManager { get; set; }
        void Register();
    }
}
