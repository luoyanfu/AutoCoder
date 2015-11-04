using System;
using System.Windows.Forms;
using PInterface;

namespace DALFactory
{
    /// <summary>
    /// abstract factory + reflection  + cache
    /// </summary>
    public sealed class DLLAccess
    {
        public static IBaseInterface CreateBaseInterface(string assemblyFile, string className)
        {
            var objType = CreateObjectLoadFile(assemblyFile, className);
            return (IBaseInterface)objType;
        }

        private static object CreateObjectLoadFile(string assemblyPath, string classNamespace)
        {
            var objType = DataCache.GetCache(classNamespace);
            if (objType != null) return objType;
            try
            {
                objType = System.Reflection.Assembly.LoadFile(assemblyPath).CreateInstance(classNamespace);
                DataCache.SetCache(classNamespace, objType);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return objType;
        }
    }
}
