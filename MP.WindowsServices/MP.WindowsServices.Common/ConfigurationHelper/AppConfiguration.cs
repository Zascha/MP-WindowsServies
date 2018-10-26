using System.Configuration;

namespace MP.WindowsServices.Common.ConfigurationHelper
{
    internal class AppConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("fileFilters")]
        public string FileFilters
        {
            get { return (string)this["fileFilters"]; }
        }
        

        [ConfigurationProperty("fileNameTemplate")]
        public string FileNameTemplate
        {
            get { return (string)this["fileNameTemplate"]; }
        }

        [ConfigurationCollection(typeof(ObservableFolderElement), AddItemName = "folder")]
        [ConfigurationProperty("observableFolders")]
        public ConfigElementCollection<ObservableFolderElement> ObservableFolders
        {
            get { return (ConfigElementCollection<ObservableFolderElement>)this["observableFolders"]; }
        }
    }
}
