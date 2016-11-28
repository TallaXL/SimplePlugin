using PluginContracts;
using System.Collections.Generic;

namespace SimplePlugin.Log
{
    public interface ILog
    {
        void AddLine(string Value);
        void AddItems(IList<TradeFlow> items);
    }
}
