/*
Author:durow
Date:2015.10.11
This is a container userd for register View,ViewModel and MessageRegister
*/
using AyxMVVM.Message;
using AyxMVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AyxMVVM
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

        /// <summary>
        /// Set the View's ViewModel to DataContext and use View's MessageRegister to register messages
        /// </summary>
        /// <param name="view">View to set it's DataContext</param>
        /// <param name="isGlobalMsg">message register as global or not</param>
        /// <param name="token">ViewModel's token</param>
        public static void SetViewModel(FrameworkElement view, bool isGlobalMsg = false, string token = "")
        {
            var vmInfo = GetViewModelInfo(view.GetType(), token);
            if (vmInfo == null) return;
            var vm = vmInfo.GetViewModelInstance();
            //set ViewModel's UIDispatcher
            vm.UIDispatcher = view.Dispatcher;
            //set View's DataContext
            view.DataContext = vm;
            //register View's message
            var msgRegister = vmInfo.GetMsgRegisterInstance();
            if (msgRegister == null) return;

            msgRegister.RegInstance = view;
            if (isGlobalMsg)
            {
                var win = view as Window;
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
