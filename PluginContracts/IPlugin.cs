using System.Collections.Generic;

namespace PluginContracts
{
    public class TradeFlow
    {
        public string DateOpen { get; set; }
        public float OpenValue { get; set; }
        public float HighValue { get; set; }
        public float LowValue { get; set; }
        public float CloseValue { get; set; }
        public int Volume { get; set; }
    }
	public interface IPlugin
	{
		string Name { get; }
        string Extension { get; }
        IList<TradeFlow> ExctractData(string FileName);
	}
}
