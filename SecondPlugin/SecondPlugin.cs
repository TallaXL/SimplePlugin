using PluginContracts;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace SecondPlugin
{
	public class SecondPlugin : IPlugin
	{
		#region IPlugin Members

		public string Name
		{
			get
			{
				return "Second Plugin";
			}
		}
        public string Extension
        {
            get
            {
                return ".csv";
            }
        }
        public IList<TradeFlow> ExctractData(string FileName)
        {
            string[] lines = System.IO.File.ReadAllLines(FileName);

            List<TradeFlow> valueList = new List<TradeFlow>();

            foreach (var buf in lines)
            {
                string[] values = buf.Split(',');
                valueList.Add(
                    new TradeFlow()
                    {
                        DateOpen = values[0],
                        OpenValue = float.Parse(values[1], CultureInfo.InvariantCulture.NumberFormat),
                        HighValue = float.Parse(values[2], CultureInfo.InvariantCulture.NumberFormat),
                        LowValue = float.Parse(values[3], CultureInfo.InvariantCulture.NumberFormat),
                        CloseValue = float.Parse(values[4], CultureInfo.InvariantCulture.NumberFormat),
                        Volume = int.Parse(values[5])
                    }
                );
            }
            return valueList;

        }
        #endregion
    }
}
