using System.ComponentModel;
using System.Threading.Tasks;
using Windows.UI;

namespace IF.Common.Metro.Mvvm
{
    public interface IViewModel : INotifyPropertyChanged
    {
        string PageTitle { get; }
        Color AccentColour { get; }

        bool Initialised { get; }
        Task AfterNavigatedToAsync(object parameter);
        Task BeforeNavigatedFromAsync();
        Task AfterPageLoadedAsync();
    }
}