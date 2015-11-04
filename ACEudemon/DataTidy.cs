using System;
using System.Data;
using ACEudemon.Entity;
using DataObtain;

namespace ACEudemon
{
    /// <summary>
    /// data tidy
    /// </summary>
    public class DataTidy
    {
        #region properties

        /// <summary>
        /// protocol datatable
        /// </summary>
        private static DataTable _dt = new DataTable();

        #endregion properties

        /// <summary>
        /// tidy parameters
        /// </summary>
        public static void TidyParameters(string filePath, OutputInfo outputInfo)
        {
            ClearResource(outputInfo);
            TidyAnalogQuantityParameters(filePath, outputInfo);
            TidySwitchingValueParameters(filePath, outputInfo);
        }

        /// <summary>
        /// clear resource
        /// </summary>
        private static void ClearResource(OutputInfo outputInfo)
        {
            outputInfo.SwitchingValueList.Clear();
            outputInfo.AnalogQuantityList.Clear();
        }

        /// <summary>
        /// tidy analog quantity parameters
        /// </summary>
        private static void TidyAnalogQuantityParameters(string filePath, OutputInfo outputInfo)
        {
            _dt = ExcelRead.ReadExcel(filePath, "analog quantity");

            if (_dt == null)
            {
                return;
            }
            
            TidyAnalogQuantityParameters(_dt, outputInfo);
        }

        /// <summary>
        /// tidy switching value parameters
        /// </summary>
        private static void TidySwitchingValueParameters(string filePath, OutputInfo outputInfo)
        {
            _dt = ExcelRead.ReadExcel(filePath, "switching value");

            if (_dt == null)
            {
                return;
            }
            
            TidySwitchingValueParameters(_dt, outputInfo);
        }

        /// <summary>
        /// tidy analog quantity parameters
        /// </summary>
        private static void TidyAnalogQuantityParameters(DataTable dt, OutputInfo outputInfo)
        {
            foreach (DataRow row in dt.Rows)
            {
                TidyAnalogQuantityParameters(row, outputInfo);
            }
        }

        /// <summary>
        /// tidy analog quantity parameters
        /// </summary>
        private static void TidyAnalogQuantityParameters(DataRow row, OutputInfo outputInfo)
        {
            var value = Convert.ToString(row[0]);

            if (!value.Contains("word"))
            {
                return;
            }

            switch (value)
            {
                case "word0":
                    outputInfo.UnitID = Convert.ToString(row[4]);
                    return;
                case "word1":
                    outputInfo.ProtocolVersion = Convert.ToString(row[4]);
                    return;
            }

            var item = new ItemInfo();

            value = value.Replace("word", "");
            var id = Convert.ToInt32(value);
            item.ID = Convert.ToString(1000 + id);

            item.Name = Convert.ToString(row[3]);
            if (item.Name.Contains("obligate"))
            {
                return;
            }

            item.Paramtype = "0";

            item.Permission = "1";
            if (Convert.ToString(row[2]) == "/")
            {
                item.Permission = "0";
            }

            value = Convert.ToString(row[5]);
            switch (value)
            {
                case "0.1":
                    item.Precision = "1";
                    break;
                case "0.01":
                    item.Precision = "2";
                    break;
                default:
                    item.Precision = "0";
                    break;
            }

            item.Valuetype = item.Precision == "0" ? "int" : "float";

            if (item.Valuetype == "int")
            {
                //word1:P2*256+P3
                item.Expression = "P" + Convert.ToString(id * 2) + "*256+P" + Convert.ToString(id * 2 + 1);
            }
            else
            {
                //word1:W2/10, precision: 0.1
                item.Expression = "W" + Convert.ToString(id * 2) + "/" + Convert.ToString(Math.Pow(10, Convert.ToInt32(item.Precision)));
            }

            item.Unit = Convert.ToString(row[6]);

            value = Convert.ToString(row[4]);
            if (!value.Contains("value"))
            {
                item.Result = Common.ReplaceChar(value) + ",";
            }

            outputInfo.AnalogQuantityList.Add(item);
        }

        /// <summary>
        /// tidy switching value parameters
        /// </summary>
        private static void TidySwitchingValueParameters(DataTable dt, OutputInfo outputInfo)
        {
            foreach (DataRow row in dt.Rows)
            {
                TidySwitchingValueParameters(row, outputInfo);
            }
        }

        /// <summary>
        /// tidy switching value parameters
        /// </summary>
        private static void TidySwitchingValueParameters(DataRow row, OutputInfo outputInfo)
        {
            var value = Convert.ToString(row[3]);

            if (!value.Contains("bit"))
            {
                return;
            }

            var item = new ItemInfo();

            value = value.Replace("bit", "");
            var id = Convert.ToInt32(value)/8;
            item.ID = Convert.ToString(5000 + Convert.ToInt32(value));

            item.Name = Convert.ToString(row[4]);
            if (item.Name.Contains("obligate"))
            {
                return;
            }

            item.Paramtype = Convert.ToString(row[6]).Contains("status") ? "0" : "1";

            item.Permission = "1";
            if (Convert.ToString(row[2]) == "/")
            {
                item.Permission = "0";
            }
            
            item.Valuetype = "bool";
            item.Precision = "0";

            //byte0~bit7:P0b7
            item.Expression = "P" + Convert.ToString(id) + "b" + Convert.ToString(Convert.ToInt32(value) % 8);

            item.Unit = "";

            if (item.Paramtype == "0")
            {
                item.Result = Common.ReplaceChar(Convert.ToString(row[5])) + ",";
            }

            outputInfo.SwitchingValueList.Add(item);
        }
    }
}
