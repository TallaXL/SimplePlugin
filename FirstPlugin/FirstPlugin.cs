using PluginContracts;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace FirstPlugin
{
	public class FirstPlugin : IPlugin
	{
		#region IPlugin Members

		public string Name
		{
			get
			{
				return "First Plugin";
			}
		}

        public string Extension
        {
            get
            {
                return ".xml";
            }
        }

        public IList<TradeFlow> ExctractData(string FileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(FileName);

            var values = xmlDoc.SelectNodes("values/value");

            List < TradeFlow > valueList = new List<TradeFlow>(values.Count);

            foreach (XmlNode v in values)
            {
                valueList.Add(
                    new TradeFlow()
                    {
                        DateOpen = v.Attributes["date"].Value,
                        OpenValue = float.Parse(v.Attributes["open"].Value, CultureInfo.InvariantCulture.NumberFormat),
                        HighValue = float.Parse(v.Attributes["high"].Value, CultureInfo.InvariantCulture.NumberFormat),
                        LowValue = float.Parse(v.Attributes["low"].Value, CultureInfo.InvariantCulture.NumberFormat),
                        CloseValue = float.Parse(v.Attributes["close"].Value, CultureInfo.InvariantCulture.NumberFormat),
                        Volume = int.Parse(v.Attributes["volume"].Value)
                    }
                );
            }
            return valueList;
        }
		#endregion
	}
}
