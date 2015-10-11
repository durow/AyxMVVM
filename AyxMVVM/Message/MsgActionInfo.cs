/*
Author:durow
Date:2015.10.11
This class is used to save message and action info
*/
using System;

namespace AyxMVVM.Message
{
    class MsgActionInfo
    {
        /// <summary>
        /// instance that receive the message
        /// </summary>
        public object RegInstance { get; internal set; }
        
        /// <summary>
        /// message name
        /// </summary>
        public string MsgName { get; internal set; }

        /// <summary>
        /// message group
        /// </summary>
        public string Group { get; internal set; }

        /// <summary>
        /// when receive this message do this action
        /// </summary>
        public Action Action { get; internal set; }

        /// <summary>
        /// execute the action
        /// </summary>
        public void Execute()
        {
            if (Action != null)
                Action();
        }
    }
}
