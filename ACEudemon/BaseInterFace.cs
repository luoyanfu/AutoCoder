using System.Windows.Forms;
using ACEudemon.Entity;
using PInterface;

namespace ACEudemon
{
    /// <summary>
    /// interface implement
    /// </summary>
    public class BaseInterFace : IBaseInterface
    {
        #region properties

        /// <summary>
        /// output information
        /// </summary>
        private static readonly OutputInfo OutputInfo = new OutputInfo();

        #endregion properties

        /// <summary>
        /// auto code
        /// </summary>
        public override void AutoCode(string filePath)
        {
            //data tidy
            DataTidy.TidyParameters(filePath, OutputInfo);

            //output code
            AcAutoCoder.OutputInternal(OutputInfo);

            MessageBox.Show("Done.");
        }
    }
}
