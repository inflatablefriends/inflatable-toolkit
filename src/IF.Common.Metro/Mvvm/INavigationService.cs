using System;
using Windows.UI.Xaml.Navigation;

namespace IF.Common.Metro.Mvvm
{
    public interface INavigationService
    {
        void Navigate(Type type);
        void Navigate(Type type, object parameter);
        void EnsureNavigated(Type pageType, object parameter);

        bool CanGoBack { get; }
        bool CanGoForward { get; }
        void GoBack();
        void GoForward();

        IView CurrentView { get; }

        event NavigatedEventHandler Navigated;
    }
}