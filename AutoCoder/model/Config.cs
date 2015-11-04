using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml;

namespace AutoCoder.model
{
    /// <summary>
    /// config
    /// </summary>
    public class Config
    {
        #region properties

        private const string FilePath = "\\data\\config.xml";
        public Dictionary<string, string> ProductDictionary = new Dictionary<string, string>();

        #endregion properties

        #region Single

        private static Config _instance;
        public static Config GetInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }

            _instance = new Config();
            _instance.GetConfigInfo();
            return _instance;
        }

        #endregion Single

        /// <summary>
        /// get configuration
        /// </summary>
        private void GetConfigInfo()
        {
            try
            {
                var document = new XmlDocument();
                document.Load(AppDomain.CurrentDomain.BaseDirectory + FilePath);
                var elements = document.GetEnumerator();
                while (elements.MoveNext())
                {
                    var xmlNode = elements.Current as XmlNode;
                    if (xmlNode.HasChildNodes)
                    {
                        SetDictionary(xmlNode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// save configuration
        /// </summary>
        private void SetDictionary(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (!childNode.Name.Equals("Item")) continue;
                var name = childNode.Attributes["Name"].Value;
                var dllName = childNode.Attributes["dllName"].Value;
                ProductDictionary.Add(name, dllName);
            }
        }
    }
}