using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace AyxMVVM
{
    public abstract class VMLocatorBase
    {
        public bool IsDesignMode
        {
            get
            {
                return DesignMode.DesignModeEnabled;
            }
        }

        public TViewModel GetViewModel<TViewModel>() where TViewModel : ViewModelBase
        {
            var instance = Activator.CreateInstance<TViewModel>();
            if (IsDesignMode)
                instance.InitTestData();
            else
                instance.InitRealData();
            return instance;
        }
    }
}
