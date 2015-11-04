
namespace DataObtain
{
    /// <summary>
    /// common methods
    /// </summary>
    public class Common
    {
        /// <summary>
        /// replace symbol
        /// </summary>
        public static string ReplaceChar(string value)
        {
            var newValue = value;
            newValue = newValue.Replace("\n", "");
            newValue = newValue.Replace("：", ":");
            newValue = newValue.Replace("，", ",");
            newValue = newValue.Trim();
            return newValue;
        }
    }
}
