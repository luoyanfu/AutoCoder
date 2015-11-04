
namespace DataObtain
{
    /// <summary>
    /// auto output
    /// </summary>
    public class AutoOutput
    {
        /// <summary>
        /// write file
        /// </summary>
        public static void WriteFile(string path, string content)
        {
            if (!System.IO.Directory.Exists(System.IO.Directory.GetParent(path).FullName))
            {
                System.IO.Directory.CreateDirectory(System.IO.Directory.GetParent(path).FullName);
            }
            System.IO.File.WriteAllText(path, content);
        }
    }
}
