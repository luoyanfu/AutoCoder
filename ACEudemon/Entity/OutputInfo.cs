using System.Collections.Generic;

namespace ACEudemon.Entity
{
    /// <summary>
    /// output information
    /// </summary>
    public class OutputInfo
    {
        /// <summary>
        /// unit ID
        /// </summary>
        public string UnitID { get; set; }

        /// <summary>
        /// protocol version
        /// </summary>
        public string ProtocolVersion { get; set; }

        /// <summary>
        /// analog quantity list
        /// </summary>
        public List<ItemInfo>AnalogQuantityList { get; set; } 

        /// <summary>
        /// switching value list
        /// </summary>
        public List<ItemInfo> SwitchingValueList { get; set; } 

        public OutputInfo()
        {
            AnalogQuantityList = new List<ItemInfo>();
            SwitchingValueList = new List<ItemInfo>();
        }
    }
}
