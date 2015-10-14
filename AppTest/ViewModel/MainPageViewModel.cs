using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using AyxMVVM;
using AyxMVVM.Command;

namespace AppTest.ViewModel
{
    class MainPageViewModel:ViewModelBase
    {
        private string _TestText = "asdfasdfasfd";

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


        private AyxCommand _CmdTest;

        /// <summary>
        /// Gets the CmdTest.
        /// </summary>
        public AyxCommand CmdTest
        {
            get
            {
                if (_CmdTest == null)
                    _CmdTest = new AyxCommand(
                    o =>
                    {
                        var dlg = new MessageDialog(TestText);
                        dlg.ShowAsync();
                    },
                    o => !string.IsNullOrEmpty(_TestText));
                return _CmdTest;
            }
        }
    }
}
