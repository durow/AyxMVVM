using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AyxMVVM.Message
{
    public class DialogMsg:MessageBase
    {
        public object Data { get; set; }
        public object Result { get; set; }

        public DialogMsg(object sender = null)
            :base(sender)
        { }
    }
}
