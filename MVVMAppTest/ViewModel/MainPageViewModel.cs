using AyxMVVM;
using AyxMVVM.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Input;
using Windows.UI.Xaml.Input;

namespace MVVMAppTest.ViewModel
{
    class MainPageViewModel:ViewModelBase
    {
        private string _TestText = "test";

        public string TestText
        {
            get { return _TestText; }
            set
            {
                if (_TestText != value)
                {
                    _TestText = value;
                    RaisePropertyChanged("TestText");
                }
            }
        }

        private AyxCommand _CmdLoaded;

        /// <summary>
        /// Gets the CmdLoaded.
        /// </summary>
        public AyxCommand CmdLoaded
        {
            get
            {
                if (_CmdLoaded == null)
                    _CmdLoaded = new AyxCommand(
                    o =>
                    {
                        TestText = o.GetType().ToString();
                    },
                    o => true);
                return _CmdLoaded;
            }
        }

        private AyxCommand _CmdMouseMove;

        /// <summary>
        /// Gets the CmdMouseMove.
        /// </summary>
        public AyxCommand CmdMouseMove
        {
            get
            {
                if (_CmdMouseMove == null)
                    _CmdMouseMove = new AyxCommand(
                    o =>
                    {
                        PointerRoutedEventArgs arg = o as PointerRoutedEventArgs;
                        if (arg == null)
                        {
                            TestText = "error";
                            return;
                        }
                        var point = arg.GetCurrentPoint(arg.OriginalSource as MainPage);
                        TestText = point.Position.X + "     " + point.Position.Y;
                    });
                return _CmdMouseMove;
            }
        }
    }
}
