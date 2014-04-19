using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Windows.UI.Core;
using IF.Common.Metro.Mvvm;

namespace IF.Common.Metro.Progress
{
    public class ProgressAggregator : PropertyChangingBase, IProgressAggregator
    {
        private ProgressToken _currentInProgress;
        private List<ProgressToken> _allInProgress;
        private Timer _timer;
        private int _loadingEventIndex;
        
        public event EventHandler<ProgressToken> ProgressChanged;

        #region Properties 

        public ProgressToken CurrentInProgress
        {
            get { return _currentInProgress; }
            set
            {
                if (_currentInProgress == value)
                {
                    return;
                }

                _currentInProgress = value;

                RaisePropertyChanged();
                RaisePropertyChanged("IsLoading");
                RaiseProgressChangedEvent(_currentInProgress);
            }
        }

        public List<ProgressToken> AllInProgress
        {
            get { return _allInProgress; }
            set
            {
                if (_allInProgress == value)
                {
                    return;
                }

                _allInProgress = value;
                RaisePropertyChanged();
            }
        }

        public bool IsLoading
        {
            get { return CurrentInProgress != null; }
        }

        #endregion

        public ProgressAggregator(CoreDispatcher dispatcher) : base(dispatcher)
        {
            AllInProgress = new List<ProgressToken>();
        }

        private void ResetTimer()
        {
            _timer = new Timer(ChangeCurrentInProgress, null, 2500, 2500);
        }

        private void DestroyTimer()
        {
            _timer = null;
            CurrentInProgress = null;
        }

        private void ChangeCurrentInProgress(object arg)
        {
            if (AllInProgress.Count == 0)
            {
                CurrentInProgress = null;
                _loadingEventIndex = 0;
                return;
            }

            if (_loadingEventIndex > (AllInProgress.Count - 1))
            {
                _loadingEventIndex = AllInProgress.Count - 1;
            }

            CurrentInProgress = AllInProgress[_loadingEventIndex];

            _loadingEventIndex++;
        }

        private void RaiseProgressChangedEvent(ProgressToken p)
        {
            if (p == null)
            {
                return;
            }

            var handler = ProgressChanged;
            if (handler != null)
            {
                handler(this, p);
            }
        }

        #region IProgressAggregator

        public void RefreshBindings()
        {
            RaisePropertyChanged("CurrentInProgress");
            RaisePropertyChanged("IsLoading");
        }

        public ProgressToken RaiseLoading(string reason, bool isIndeterminate)
        {
            var token = new ProgressToken(reason, Dispatcher, isIndeterminate);
            RaiseLoading(token);
            return token;
        }

        public ProgressToken RaiseLoading(string reason, bool isIndeterminate, CancellationTokenSource cts, ICommand cancelCommand)
        {
            var token = new ProgressToken(reason, Dispatcher, isIndeterminate, cts, cancelCommand);
            RaiseLoading(token);
            return token;
        }

        public void RaiseLoading(ProgressToken e)
        {
            if (AllInProgress.Any(l => l.Id == e.Id))
            {
                // operation is already in progress
                if (e.Value == 100)
                {
                    // operation has finished - remove it from the queue
                    AllInProgress.RemoveAll(l => l.Id == e.Id);

                    if (AllInProgress.Count <= 1)
                    {
                        if (AllInProgress.Count == 1)
                        {
                            CurrentInProgress = AllInProgress.First();
                        }

                        DestroyTimer();
                    }
                }
                else
                {
                    // operation still in progress - update existing
                    for (var i = 0; i < AllInProgress.Count; i++)
                    {
                        if (AllInProgress[i].Id == e.Id)
                        {
                            AllInProgress[i] = e;

                            RaiseProgressChangedEvent(e);
                        }
                    }
                }
            }
            else
            {
                AllInProgress.Add(e);
                ChangeCurrentInProgress(null);

                if (AllInProgress.Count > 1)
                {
                    ResetTimer();
                }
            }
        }

        public void Finalise(ProgressToken token)
        {
            if (!AllInProgress.Contains(token))
            {
                return;
            }

            token.Value = 100;
            RaiseLoading(token);
        }

        #endregion

    }
}
