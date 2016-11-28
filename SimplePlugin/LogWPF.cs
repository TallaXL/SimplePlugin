using SimplePlugin.Log;
using System.Windows.Controls;
using PluginContracts;
using System.Collections.Generic;

namespace SimplePlugin
{
    public class WPFLog : ILog
    {
        private object threadLock = new object();

        private TextBox _view;
        private ItemCollection _itemCollection;
        public WPFLog(TextBox view, ItemCollection itemCollection)
        {
            _view = view;
            _itemCollection = itemCollection;
        }

        public void AddItems(IList<TradeFlow> items)
        {
            lock (threadLock)
            {
                _itemCollection.Dispatcher.Invoke(() =>
                {
                    foreach (var item in items)
                    {
                        _itemCollection.Add(item);

                    }
                });
            }
        }

        public void AddLine(string Value)
        {
            _view.Dispatcher.Invoke(() => { _view.AppendText(Value + "\r\n"); });
        }
    }
}
