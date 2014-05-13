using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Core;

namespace IF.Common.Metro.Mvvm
{
    public class PropertyChangingBase : INotifyPropertyChanged
    {
        protected CoreDispatcher Dispatcher { get; private set; }

        protected PropertyChangingBase(CoreDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [Annotations.NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (Dispatcher == null)
            {
                return;
            }

            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                Dispatcher.RunIdleAsync(d => handler(this, new PropertyChangedEventArgs(propertyName)));
            }
        }

        #endregion
    }
}