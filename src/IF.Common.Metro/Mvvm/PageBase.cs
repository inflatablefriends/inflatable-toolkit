using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace IF.Common.Metro.Mvvm
{
    public class PageBase : Page
    {
        public IViewModel ViewModel { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.AfterNavigatedTo(e);
            }

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (ViewModel != null)
            {
                ViewModel.BeforeNavigatedFrom();
            }
        }
    }
}