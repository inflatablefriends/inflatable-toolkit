using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Xaml.Navigation;
using IF.Common.Metro.Progress;

namespace IF.Common.Metro.Mvvm
{
    public class ViewModelBase : PropertyChangingBase, IViewModel
    {
        public string PageTitle { get; private set; }

        public IProgressAggregator Progress { get; private set; }

        public bool DesignModeEnabled
        {
            get { return DesignMode.DesignModeEnabled; }
        }

        public ViewModelBase() : base(null)
        {
            
        }

        public ViewModelBase(CoreDispatcher dispatcher, IProgressAggregator p) : base(dispatcher)
        {
            Progress = p;
            PageTitle = this.GetType().Name;
        }

        public virtual void AfterNavigatedTo(object parameter)
        {
        }

        public virtual void BeforeNavigatedFrom()
        {
        }

        public virtual void AfterPageLoaded()
        {
        }
    }
}