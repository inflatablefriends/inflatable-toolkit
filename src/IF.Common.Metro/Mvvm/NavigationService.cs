using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace IF.Common.Metro.Mvvm
{
    public class NavigationService : INavigationService
    {
        private readonly Frame _frame;

        public NavigationService(Frame frame)
        {
            _frame = frame;
            _frame.Navigated += OnFrameNavigated;
        }
        
        private void OnFrameNavigated(object sender, NavigationEventArgs e)
        {
            var view = e.Content as IView;
            if (view == null)
                return;

            //var navMsg = new NavigationMessage()
            //{
            //    Sender = this,
            //    NewView = view,
            //    Parameter = e.Parameter,
            //    NavigationMode = (int)e.NavigationMode
            //};

            //EventManager.Current.Publish(navMsg);

            //Anything that the parent needs to be notified should happen in of after this me thod
            var viewModel = view.ViewModel;
            if (viewModel != null)
            {
                var copy = e.Parameter;

                Task.Run(() => viewModel.AfterNavigatedToAsync(copy));
            }

            var handler = Navigated;
            if (handler != null)
            {
                Navigated(sender, e);
            }
        }

        public event NavigatedEventHandler Navigated;

        public void Navigate(Type pageType)
        {
            DisposePreviousView();
            _frame.Navigate(pageType);
        }

        public void Navigate(Type pageType, object parameter)
        {
            DisposePreviousView();
            _frame.Navigate(pageType, parameter);
        }

        private void DisposePreviousView()
        {
            var currentView = this.CurrentView;
            var currentViewDisposable = currentView as IDisposable;
            if (currentViewDisposable != null)
            {
                currentViewDisposable.Dispose();
                currentViewDisposable = null;
            } //view model is disposed in the view implementation
        }

        public void EnsureNavigated(Type pageType, object parameter)
        {
            var currentView = this.CurrentView;
            if (currentView == null || currentView.GetType() != pageType)
            {
                Navigate(pageType, parameter);
            }
        }

        public IView CurrentView
        {
            get { return _frame.Content as IView; }
        }


        public bool CanGoBack
        {
            get { return _frame != null && _frame.CanGoBack; }
        }

        public void GoBack()
        {
            // Use the navigation frame to return to the previous page
            if (_frame != null && _frame.CanGoBack) _frame.GoBack();
        }

        public bool CanGoForward
        {
            get { return _frame != null && _frame.CanGoForward; }
        }

        public void GoForward()
        {
            // Use the navigation frame to return to the previous page
            if (_frame != null && _frame.CanGoForward) _frame.GoForward();
        }

    }
}