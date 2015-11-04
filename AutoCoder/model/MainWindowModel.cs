using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AutoCoder.model
{
    /// <summary>
    /// main window
    /// </summary>
    public class MainWindowModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged

        #region window

        /// <summary>
        /// product list
        /// </summary>
        private List<string> _productList;
        public List<string> ProductList
        {
            get { return _productList ?? (_productList = Config.GetInstance().ProductDictionary.Keys.ToList()); }
        }

        /// <summary>
        /// product selectedindex
        /// </summary>
        private int _productSelectedIndex = 0;
        public int ProductSelectedIndex
        {
            get { return _productSelectedIndex; }
            set
            {
                _productSelectedIndex = value;
                _productName = ProductList[_productSelectedIndex];
                _productDllName = Config.GetInstance().ProductDictionary[_productName];
                AssemblyFile = StartPath + "dll\\" + string.Format("{0}.dll", _productDllName);
                ClassNamespace = string.Format("{0}.BaseInterFace", _productDllName);
                OnPropertyChanged("ProductSelectedIndex");
            }
        }

        /// <summary>
        /// product name
        /// </summary>
        private string _productName = "ACEudemon";

        /// <summary>
        /// dll name
        /// </summary>
        private string _productDllName = "ACEudemon";

        /// <summary>
        /// start path
        /// </summary>
        private static readonly string StartPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// dll path
        /// </summary>
        public string AssemblyFile = StartPath + "dll\\ACEudemon.dll";

        /// <summary>
        /// namespace
        /// </summary>
        public string ClassNamespace = "ACEudemon.BaseInterFace";

        #endregion window
    }
}
