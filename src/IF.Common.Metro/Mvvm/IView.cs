using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace IF.Common.Metro.Mvvm
{
    public interface IView
    {
        IViewModel ViewModel { get; }
    }    
}