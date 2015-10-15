/*
Author:durow
Date:2015.10.11
Use this class to get MainWindow's Dispatcher
*/

using Windows.UI.Core;

namespace AyxMVVM.Threading
{
    public class DispatcherHelper
    {
        /// <summary>
        /// MainWindow's Dispatcher
        /// </summary>
        public static CoreDispatcher UIDispatcher { get; set; }
    }
}
