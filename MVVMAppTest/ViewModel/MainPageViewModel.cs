using AyxMVVM;
using AyxMVVM.Command;
using MVVMAppTest.Model;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Input;

namespace MVVMAppTest.ViewModel
{
    class MainPageViewModel:ViewModelBase
    {
        private string _TestText;

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

        private TestData _SelectedData;

        public TestData SelectedData
        {
            get { return _SelectedData; }
            set
            {
                if (_SelectedData != value)
                {
                    _SelectedData = value;
                    RaisePropertyChanged("SelectedData");
                    CmdDelete.RaiseCanExecuteChanged();
                }
            }
        }


        private ObservableCollection<TestData> _TestDataList;

        public ObservableCollection<TestData> TestDataList
        {
            get { return _TestDataList; }
            set
            {
                if (_TestDataList != value)
                {
                    _TestDataList = value;
                    RaisePropertyChanged("TestDataList");
                }
            }
        }

        public MainPageViewModel()
        {
            TestDataList = new ObservableCollection<TestData>(TestData.InitData());
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

        private AyxCommand _CmdTest;

        private AyxCommand _CmdAdd;

        /// <summary>
        /// Gets the CmdAdd.
        /// </summary>
        public AyxCommand CmdAdd
        {
            get
            {
                if (_CmdAdd == null)
                    _CmdAdd = new AyxCommand(
                    o =>
                    {
                        TestDataList.Add(TestData.GetInstance());
                    });
                return _CmdAdd;
            }
        }

        private AyxCommand _CmdDelete;

        /// <summary>
        /// Gets the CmdDelete.
        /// </summary>
        public AyxCommand CmdDelete
        {
            get
            {
                if (_CmdDelete == null)
                    _CmdDelete = new AyxCommand(
                    o =>
                    {
                        if (SelectedData != null)
                            TestDataList.Remove(SelectedData);
                    },
                    o=> {
                        return SelectedData != null;
                    });
                return _CmdDelete;
            }
        }

        private AyxCommand _CmdClear;

        /// <summary>
        /// Gets the CmdClear.
        /// </summary>
        public AyxCommand CmdClear
        {
            get
            {
                if (_CmdClear == null)
                    _CmdClear = new AyxCommand(
                    o =>
                    {
                        TestDataList.Clear();
                    });
                return _CmdClear;
            }
        }
    }
}
