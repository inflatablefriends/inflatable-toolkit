using System;

namespace IF.Common.Metro.Mvvm
{
    public interface IView
    {
        IViewModel ViewModel { get; }
    }
}