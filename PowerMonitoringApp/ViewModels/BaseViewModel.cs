//using CommunityToolkit.Mvvm.ComponentModel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PowerMonitoringApp.ViewModels
//{
//    public partial class BaseViewModel : ObservableObject
//    {
//        [ObservableProperty]
//        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
//        bool _isBusy;

//        [ObservableProperty]
//        string _title;

//        public bool IsNotBusy => !IsBusy;
//    }
//}

using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace PowerMonitoringApp.ViewModels
{
    public partial class BaseViewModel : ObservableObject, IDisposable
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool _isBusy;

        [ObservableProperty]
        string _title;

        public bool IsNotBusy => !IsBusy;

        // Virtual Dispose method for cleanup
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose managed resources here if any
            }
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Prevent finalizer from running
        }
    }
}
