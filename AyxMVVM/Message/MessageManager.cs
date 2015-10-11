/*
Author:durow
Date:2015.10.11
MessageManager is used to register and send message
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AyxMVVM.Message
{
    public class MessageManager : IMessageManager
    {
        private static MessageManager _default;

        /// <summary>
        /// This static instance is used for global messages
        /// </summary>
        public static MessageManager Default
        {
            get
            {
                if (_default == null)
                    _default = new MessageManager();
                return _default;
            }
        }

        /// <summary>
        /// Save the registered message actions
        /// </summary>
        private readonly List<MsgActionInfo> _messageList = new List<MsgActionInfo>();

        /// <summary>
        /// Register message
        /// </summary>
        /// <param name="regInstance">instance receive the message</param>
        /// <param name="msgName">message name</param>
        /// <param name="action">message action</param>
        /// <param name="group">message group</param>
        public void Register(object regInstance, string msgName, Action action, string group = "")
        {
            _messageList.Add(new MsgActionInfo
            {
                RegInstance = regInstance,
                MsgName = msgName,
                Action = action,
                Group = group
            });
        }

        /// <summary>
        /// Register message
        /// </summary>
        /// <typeparam name="T">message parameter type</typeparam>
        /// <param name="regInstance">instance receive the message</param>
        /// <param name="msgName">message name</param>
        /// <param name="action">message action</param>
        /// <param name="group">message group</param>
        public void Register<T>(object regInstance, string msgName, Action<T> action, string group = "")
        {
            _messageList.Add(new MsgActionInfo<T>
            {
                RegInstance = regInstance,
                MsgName = msgName,
                Action = action,
                Group = group
            });
        }

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="msgName">message name</param>
        /// <param name="targetType">target type that receive the message</param>
        /// <param name="group">message group</param>
        public void SendMsg(string msgName, Type targetType = null, string group = "")
        {
            var actions = GetMsgActionInfo(msgName, targetType, group);

            foreach (var item in actions)
            {
                item.Execute();
            }
        }

        /// <summary>
        /// Send message
        /// </summary>
        /// <typeparam name="T">message parameter type</typeparam>
        /// <param name="msgName">message name</param>
        /// <param name="msgArgs">message parameter</param>
        /// <param name="targetType">target type that receive the message</param>
        /// <param name="group">message group</param>
        public void SendMsg<T>(string msgName, T msgArgs, Type targetType = null, string group = "")
        {
            var actions = GetMsgActionInfo(msgName, targetType, group);
            foreach (var item in actions)
            {
                var msgAction = item as MsgActionInfo<T>;
                if (msgAction != null)
                    msgAction.Execute(msgArgs);
            }
        }

        /// <summary>
        /// Unregister messages
        /// </summary>
        /// <param name="regInstance">receiver instance</param>
        public void UnRegister(object regInstance)
        {
            var msgActions = _messageList.Where(m => m.RegInstance == regInstance).ToList();
            foreach (var item in msgActions)
            {
                _messageList.Remove(item);
            }
        }

        /// <summary>
        /// Clear the message list
        /// </summary>
        public void Clear()
        {
            _messageList.Clear();
        }

        private IEnumerable<MsgActionInfo> GetMsgActionInfo(string msgName, Type targetType, string group)
        {
            if (targetType == null)
                return _messageList.Where(m =>
                    m.MsgName == msgName
                    && m.Group == group);
            else
            {
                return _messageList.Where(m =>
                    m.MsgName == msgName
                    && m.Group == group
                    && m.RegInstance.GetType() == targetType);
            }
        }

        /// <summary>
        /// When window close,unregister messages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void WindowClose(object sender, EventArgs e)
        {
            //注销窗体的消息
            UnRegister(sender);
            //注销ViewModel的消息
            var win = sender as FrameworkElement;
            if (win != null)
                UnRegister(win.DataContext);
        }
    }
}
