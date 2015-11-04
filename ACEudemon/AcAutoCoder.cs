using System;
using ACEudemon.Entity;
using DataObtain;

namespace ACEudemon
{
    /// <summary>
    /// AC Eudemon auto coder
    /// </summary>
    public class AcAutoCoder
    {
        #region properties

        /// <summary>
        /// template path
        /// </summary>
        private static readonly string TemplatePath = AppDomain.CurrentDomain.BaseDirectory + "template\\ACEudemon\\";

        /// <summary>
        /// output file path
        /// </summary>
        private static readonly string OutputPath = AppDomain.CurrentDomain.BaseDirectory + "output\\ACEudemon\\";

        #endregion properties

        #region methods

        /// <summary>
        /// output code
        /// </summary>
        public static void OutputInternal(OutputInfo outputInfo)
        {
            OutputAnalogQuantities(outputInfo);
            OutputSwitchingValues(outputInfo);
        }

        /// <summary>
        /// initialize template
        /// </summary>
        private static Antlr4.StringTemplate.Template InitializeTemplate()
        {
            var templateName = TemplatePath + "XmlScript.stg";
            Antlr4.StringTemplate.TemplateGroup templateGroup = new Antlr4.StringTemplate.TemplateGroupFile(templateName);
            templateGroup.TrackCreationEvents = true;
            Antlr4.StringTemplate.Template template = templateGroup.GetInstanceOf("XmlScript");
            return template;
        }

        /// <summary>
        /// output analog quantity
        /// </summary>
        private static void OutputAnalogQuantities(OutputInfo outputInfo)
        {
            Antlr4.StringTemplate.Template template = InitializeTemplate();
            template.Add("ItemInfos", outputInfo.AnalogQuantityList);
            AutoOutput.WriteFile(OutputPath + "AnalogQuantities.xml", template.Render());
        }

        /// <summary>
        /// output switching value
        /// </summary>
        private static void OutputSwitchingValues(OutputInfo outputInfo)
        {
            Antlr4.StringTemplate.Template template = InitializeTemplate();
            template.Add("ItemInfos", outputInfo.SwitchingValueList);
            AutoOutput.WriteFile(OutputPath + "SwitchingValues.xml", template.Render());
        }

        #endregion methods
    }
}
