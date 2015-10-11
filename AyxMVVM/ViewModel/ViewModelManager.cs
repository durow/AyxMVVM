/*
Author:durow
Date:2015.09.11
This is a container userd for register View,ViewModel and MessageRegister
*/
using AyxMVVM.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AyxMVVM.ViewModel
{
    public class ViewModelManager
    {
        private static List<ViewModelInfo> _viewModelInfoList = new List<ViewModelInfo>();

        /// <summary>
        /// Register View,ViewModel and MessageRegister
        /// </summary>
        /// <typeparam name="TView">View's type</typeparam>
        /// <typeparam name="TViewModel">ViewModel's type</typeparam>
        /// <typeparam name="TMsgRegister">MessageRegister's type</typeparam>
        /// <param name="token">ViewModel's token</param>
        public static void Register<TView, TViewModel, TMsgRegister>(string token = "")
        {
            var vmInfo = new ViewModelInfo(
                typeof(TView),
                typeof(TViewModel),
                typeof(TMsgRegister),
                token);
            _viewModelInfoList.Add(vmInfo);
        }

        /// <summary>
        /// Register View and ViewModel without MessageRegister
        /// </summary>
        /// <typeparam name="TView">View's type</typeparam>
        /// <typeparam name="TViewModel">ViewModel's type</typeparam>
        /// <param name="token">ViewModel's token</param>
        public static void Register<TView, TViewModel>(string token = "")
        {
            var vmInfo = new ViewModelInfo(
                typeof(TView),
                typeof(TViewModel),
                token: token);
            _viewModelInfoList.Add(vmInfo);
        }

        /// <summary>
        /// Get the View's ViewModel
        /// </summary>
        /// <typeparam name="TView">View's type</typeparam>
        /// <param name="token">ViewModel's token</param>
        /// <returns>ViewModel</returns>
        public static object GetViewModel<TView>(string token = "")
        {
            try
            {
                var vmType = GetViewModelInfo(typeof(TView), token).ViewModelType;
                return vmType.Assembly.CreateInstance(vmType.FullName);
            }
            catch
            {
                return null;
            }
        }

        public static void SetViewModel(FrameworkElement fe, bool isGlobalMsg = false, string token = "")
        {
            var vmInfo = GetViewModelInfo(fe.GetType(), token);
            if (vmInfo == null) return;
            var vm = vmInfo.GetViewModelInstance();
            //set ViewModel's UIDispatcher
            vm.UIDispatcher = fe.Dispatcher;
            //set View's DataContext
            fe.DataContext = vm;
            //register View's message
            var msgRegister = vmInfo.GetMsgRegisterInstance();
            if (msgRegister == null) return;

            msgRegister.RegInstance = fe;
            if (isGlobalMsg)
            {
                var win = fe as Window;
                if (win == null)
                {
                    throw new Exception("only can set a Window's message as global!");
                }
                vm.MsgManager = MessageManager.Default;
                win.Closed += MessageManager.Default.WindowClose;
            }
            else
            {
                vm.MsgManager = new MessageManager();
            }
            msgRegister.MsgManager = vm.MsgManager;
            msgRegister.Register();
        }

        private static ViewModelInfo GetViewModelInfo(Type viewType, string token = "")
        {
            try
            {
                return _viewModelInfoList
                    .Where(p => p.ViewType == viewType && p.Token == token)
                    .First();
            }
            catch
            {
                return null;
            }
        }

    }
}
