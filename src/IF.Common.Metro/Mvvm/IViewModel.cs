using System.ComponentModel;
using Windows.UI.Xaml.Navigation;

namespace IF.Common.Metro.Mvvm
{
    public interface IViewModel : INotifyPropertyChanged
    {
        string PageTitle { get; }

        void AfterNavigatedTo(object parameter);
        void BeforeNavigatedFrom();
        void AfterPageLoaded();
    }
}