using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AyxMVVM.Message
{
    public class MessageBase
    {
        public object Sender { get; private set; }

        public MessageBase(object sender = null)
        {
            Sender = sender;
        }
    }
}
