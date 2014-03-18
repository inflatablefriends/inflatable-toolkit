using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;

namespace IF.Common.Metro.Progress
{
    public interface IProgressAggregator
    {
        ProgressToken CurrentInProgress { get; set; }
        List<ProgressToken> AllInProgress { get; set; }
        bool IsLoading { get; }
        void Finalise(ProgressToken token);

        void RefreshBindings();

        ProgressToken RaiseLoading(string reason, bool isIndeterminate);
        ProgressToken RaiseLoading(string reason, bool isIndeterminate, CancellationTokenSource cts, ICommand cancelCommand);
        
        void RaiseLoading(ProgressToken e);

        event EventHandler<ProgressToken> ProgressChanged;
    }
}