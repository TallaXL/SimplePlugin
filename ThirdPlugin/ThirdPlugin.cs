using PluginContracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdPlugin
{
    public class ThirdPlugin : IPlugin
    {
        public string Name
        {
            get
            {
                return "Third Plugin";
            }
        }
        public string Extension
        {
            get
            {
                return ".txt";
            }
        }
        public IList<TradeFlow> ExctractData(string FileName)
        {
            string[] lines = System.IO.File.ReadAllLines(FileName);

            List<TradeFlow> valueList = new List<TradeFlow>();

            //remove header in array
            foreach (var buf in lines.Skip(1))
            {
                string[] values = buf.Split(';');
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
    }
}
