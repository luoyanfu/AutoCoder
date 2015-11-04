
namespace ACEudemon.Entity
{
    /// <summary>
    /// item information
    /// </summary>
    public class ItemInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// parameter type
        /// 0: status
        /// 1: fault
        /// </summary>
        public string Paramtype { get; set; }

        /// <summary>
        /// authority
        /// 0: factory
        /// 1: user
        /// </summary>
        public string Permission { get; set; }

        /// <summary>
        /// value type：bool、int、float
        /// </summary>
        public string Valuetype { get; set; }

        /// <summary>
        /// precision
        /// </summary>
        public string Precision { get; set; }

        /// <summary>
        /// expression
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// unit
        /// </summary>
        public string Unit { get; set; }
        
        /// <summary>
        /// result
        /// </summary>
        public string Result { get; set; }
    }
}
