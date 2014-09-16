using System;
using System.Threading;
using System.Windows.Input;
using Windows.UI.Core;
using IF.Common.Metro.Mvvm;

namespace IF.Common.Metro.Progress
{
    public class ProgressToken : PropertyChangingBase
    {
        private int _value;
        private bool _complete;
        private bool _isIndeterminate;

        public Guid Id { get; private set; }
        public string Reason { get; set; }

        #region Optional properties

        public string FinishedMessage { get; set; }
        public Uri ImageUri { get; set; }

        /// <summary>
        /// Set to null for indeterminate send 100 to complete
        /// </summary>
        public int Value
        {
            get { return _value; }
            set
            {
                if (_value == value)
                {
                    return;
                }

                _value = value;
                RaisePropertyChanged();
            }
        }

        public CancellationTokenSource CancellationTokenSource { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public bool CanCancel
        {
            get { return CancellationTokenSource != null && CancelCommand != null; }
        }

        public bool Complete
        {
            get { return _complete; }
            set
            {
                if (_complete == value)
                {
                    return;
                }

                _complete = value;
                RaisePropertyChanged();
            }
        }

        public bool IsIndeterminate
        {
            get { return _isIndeterminate; }
            set
            {
                if (_isIndeterminate == value)
                {
                    return;
                }

                _isIndeterminate = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        private void Initialise(string reason, bool isIndeterminate, CancellationTokenSource cts, ICommand command)
        {
            Id = Guid.NewGuid();
            Reason = reason;
            IsIndeterminate = isIndeterminate;
            CancellationTokenSource = cts;
            CancelCommand = command;
        }

        public ProgressToken(string reason, CoreDispatcher dispatcher)
            : base(dispatcher)
        {
            Initialise(reason, false, null, null);
        }

        public ProgressToken(string reason, CoreDispatcher dispatcher, bool isIndeterminate)
            : base(dispatcher)
        {
            Initialise(reason, isIndeterminate, null, null);
        }

        public ProgressToken(string reason, CoreDispatcher dispatcher, bool isIndeterminate, CancellationTokenSource cts, ICommand cancelCommand)
            : base(dispatcher)
        {
            Initialise(reason, isIndeterminate, cts, cancelCommand);
        }

        
    }
}