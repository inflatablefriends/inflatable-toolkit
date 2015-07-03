using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace IF.Common.Metro.UI
{
    /// <summary>
    /// With thanks to Rudy Huyn
    /// http://www.rudyhuyn.com/blog/2015/06/10/listview-how-to-prevent-glitches-during-scrolling/
    /// </summary>
    public class StableListView : ListView
    {
        public StableListView()
        {
            SizeChanged += PerfectScrollListView_SizeChanged;
        }

        private void PerfectScrollListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var root = ItemsPanelRoot;
            if (root != null)
            {
                root.Width = e.NewSize.Width;
            }
        }
    }
}
